using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;


    [Route("api/[controller]")]
    [ApiController]
    public class EmailmstrController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmailmstrController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FormData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emailmstr>>> GetFormData()
        {

        Console.WriteLine(_context.Emailmstr);

            if (_context.Emailmstr == null)
            {
                return NotFound();
            }
            return await _context.Emailmstr.ToListAsync();
        }

        // GET: api/FormData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emailmstr>> GetFormData(int id)
        {
            var formData = await _context.Emailmstr.FindAsync(id);

            if (formData == null)
            {
                return NotFound();
            }

            return formData;
        }

        // POST: api/FormData
        [HttpPost]
        public async Task<ActionResult<Emailmstr>> PostFormData(Emailmstr formData)
        {
            _context.Emailmstr.Add(formData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFormData), new { DocId = formData.DocId }, formData);
        }

        // PUT: api/FormData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormData(int id, Emailmstr formData)
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
            var formData = await _context.Emailmstr.FindAsync(id);

            if (formData == null)
            {
                return NotFound();
            }

            _context.Emailmstr.Remove(formData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormDataExists(int id)
        {
            return _context.Emailmstr.Any(e => e.DocId == id);
        }
    }

