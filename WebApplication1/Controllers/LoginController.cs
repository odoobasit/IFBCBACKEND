using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;

    public LoginController(ApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest == null)
        {
            return BadRequest(new { Message = "Invalid client request" });
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);
        if (user == null)
        {
            return Unauthorized(new { Message = "User not found! Make sure the email is correct." });
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

        if (!isPasswordValid)
        {
            return Unauthorized(new { Message = "Incorrect password! Please try again." });
        }

        try
        {
            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            // Log the exception here
            return StatusCode(500, new { Message = "Internal server error", Detail = ex.Message });
        }
    }
}
