using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 
        private readonly TokenService _tokenService;
        private readonly OtpService _otpService;


        public UsersController(ApplicationDbContext context, TokenService tokenService, OtpService otpService)
        {
            _context = context;
            _tokenService = tokenService;
            _otpService = otpService;
        }

        // GET: api/RequestData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetRequestData()
        {
            return await _context.Users.ToListAsync();
        }

        [Authorize]
        [HttpGet("userdata")]
        public async Task<ActionResult<Users>> GetUserData()
        {
            // Log all claims for debugging
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Retrieve the user's email from the claims in the token
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userEmail == null)
            {
                return Unauthorized(); // Return unauthorized if the email claim is not found
            }

            // Retrieve the user from the database using the email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound(); // Return not found if the user does not exist
            }

            return user; // Return the user data
        }



        // POST: api/RequestData
        [HttpPost]
        public async Task<ActionResult<Users>> PostRequestData(Users users)
        {
            if (users.Email == null)
            {
                return BadRequest();
            }

            users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);

            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            try
            {
                var token = _tokenService.GenerateToken(users);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { Message = "Internal server error", Detail = ex.Message });
            }
            //// Generate OTP
            //_otpService.GenerateAndSendOtp(users.Email,users.FirstName);

            //// Return a response indicating that the OTP has been sent
            //return Ok(new { Message = "OTP has been sent to your email address" });
        }

        public class VerifyOtpRequest
        {
            public Users RequestData { get; set; }
            public string UserOTP { get; set; }
        }

            // POST: api/users/VerifyOTP
            [HttpPost("VerifyOTP")]
            public async Task<ActionResult<Users>> VerifyOTPAndStoreData([FromBody] VerifyOtpRequest verifyOtpRequest)
            {
                if (verifyOtpRequest.RequestData == null || string.IsNullOrEmpty(verifyOtpRequest.UserOTP))
                {
                    return BadRequest();
                }

                var requestData = verifyOtpRequest.RequestData;
                var userOTP = verifyOtpRequest.UserOTP;

                // Verify the OTP provided by the user
                if (!_otpService.VerifyOtp(requestData.Email, userOTP))
                {
                    return BadRequest("Invalid OTP");
                }

                // Hash the password before saving
                requestData.Password = BCrypt.Net.BCrypt.HashPassword(requestData.Password);

                _context.Users.Add(requestData);
                await _context.SaveChangesAsync();
                try
                {
                    var token = _tokenService.GenerateToken(requestData);
                    return Ok(new { Token = token });
                }
                catch (Exception ex)
                {
                    // Log the exception here
                    return StatusCode(500, new { Message = "Internal server error", Detail = ex.Message });
                }
            }
        


        // PUT: api/RequestData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestData(int id, Users requestData)
        {
            if (id != requestData.DocId)
            {
                return BadRequest();
            }
            requestData.Password = BCrypt.Net.BCrypt.HashPassword(requestData.Password);

            _context.Entry(requestData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/RequestData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestData(int id)
        {
            var requestData = await _context.Users.FindAsync(id);
            if (requestData == null)
            {
                return NotFound();
            }

            _context.Users.Remove(requestData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestDataExists(int id)
        {
            return _context.Users.Any(e => e.DocId == id);
        }
    }
}
