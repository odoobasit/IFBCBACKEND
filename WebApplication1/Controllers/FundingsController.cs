using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FundingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fundings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funding>>> GetFundings()
        {
            return await _context.Fundings.ToListAsync();
        }

        // GET: api/Fundings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funding>> GetFunding(int id)
        {
            var funding = await _context.Fundings.FindAsync(id);

            if (funding == null)
            {
                return NotFound();
            }

            return funding;
        }

        private void SendEmail(string email,string name)
        {
            var fromAddress = new MailAddress("charliechaplin565@gmail.com", "IFBC");
            var toAddress = new MailAddress(email, name);
            const string fromPassword = "nvtv cbdb vhsj evjf";
            const string subject = "Subject";
            const string body = "Candidate registration successfull!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        // POST: api/Fundings
        [HttpPost]
        public async Task<ActionResult<Funding>> PostFunding(Funding funding)
        {
            _context.Fundings.Add(funding);
            await _context.SaveChangesAsync();
            SendEmail(funding.Email, funding.Name);
            return CreatedAtAction("GetFunding", new { id = funding.DocId }, funding);
        }

        // PUT: api/Fundings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFunding(int id, Funding funding)
        {
            if (id != funding.DocId)
            {
                return BadRequest();
            }

            _context.Entry(funding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundingExists(id))
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

        // DELETE: api/Fundings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFunding(int id)
        {
            var funding = await _context.Fundings.FindAsync(id);
            if (funding == null)
            {
                return NotFound();
            }

            _context.Fundings.Remove(funding);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FundingExists(int id)
        {
            return _context.Fundings.Any(e => e.DocId == id);
        }
    }
}
