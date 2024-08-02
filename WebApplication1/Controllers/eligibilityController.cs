using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;


[Route("api/[controller]")]
[ApiController]
public class eligibilityController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public eligibilityController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/FormData
    [HttpGet]
    public async Task<ActionResult<IEnumerable<eligibility>>> GetFormData()


    {
        Console.WriteLine(_context.eligibility);

        if (_context.eligibility == null)
        {
            return NotFound();
        }
        return await _context.eligibility.ToListAsync();
    }

    // GET: api/FormData/5
    [HttpGet("{id}")]
    public async Task<ActionResult<eligibility>> GetFormData(int id)
    {
        var formData = await _context.eligibility.FindAsync(id);

        if (formData == null)
        {
            return NotFound();
        }

        return formData;
    }

    // POST: api/FormData
    [HttpPost]
    public async Task<ActionResult<eligibility>> PostFormData(eligibility formData)
    {
        _context.eligibility.Add(formData);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFormData), new { DocId = formData.docid }, formData);
    }

    // PUT: api/FormData/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormData(int id, eligibility formData)
    {
        if (id != formData.docid)
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
        var formData = await _context.eligibility.FindAsync(id);

        if (formData == null)
        {
            return NotFound();
        }

        _context.eligibility.Remove(formData);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FormDataExists(int id)
    {
        return _context.eligibility.Any(e => e.docid == id);
    }
}

