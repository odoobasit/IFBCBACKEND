using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;


    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseIssueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FranchiseIssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FormData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseIssue>>> GetFormData()


        {

        Console.WriteLine(_context.FranchiseIssue);

            if (_context.FranchiseIssue == null)
            {
                return NotFound();
            }
            return await _context.FranchiseIssue.ToListAsync();
        }

        // GET: api/FormData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseIssue>> GetFormData(int id)
        {
            var formData = await _context.FranchiseIssue.FindAsync(id);

            if (formData == null)
            {
                return NotFound();
            }

            return formData;
        }

        // POST: api/FormData
        [HttpPost]
        public async Task<ActionResult<FranchiseIssue>> PostFormData(FranchiseIssue formData)
        {
            _context.FranchiseIssue.Add(formData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFormData), new { DocId = formData.DocId }, formData);
        }

        // PUT: api/FormData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormData(int id, FranchiseIssue formData)
        {
            if (id != formData.DocId)
            {
                return BadRequest();
            }

            _context.Entry(formData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormDataExists(id))
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

        // DELETE: api/FormData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormData(int id)
        {
            var formData = await _context.FranchiseIssue.FindAsync(id);

            if (formData == null)
            {
                return NotFound();
            }

            _context.FranchiseIssue.Remove(formData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormDataExists(int id)
        {
            return _context.FranchiseIssue.Any(e => e.DocId == id);
        }
    }

