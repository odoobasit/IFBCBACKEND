using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;


[Route("api/[controller]")]
[ApiController]
public class experienceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public experienceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/FormData
    [HttpGet]
    public async Task<ActionResult<IEnumerable<experience>>> GetFormData()


    {
        Console.WriteLine(_context.experience);

        if (_context.experience == null)
        {
            return NotFound();
        }
        return await _context.experience.ToListAsync();
    }

    // GET: api/FormData/5
    [HttpGet("{id}")]
    public async Task<ActionResult<experience>> GetFormData(int id)
    {
        var formData = await _context.experience.FindAsync(id);

        if (formData == null)
        {
            return NotFound();
        }

        return formData;
    }

    // POST: api/FormData
    [HttpPost]
    public async Task<ActionResult<experience>> PostFormData(experience formData)
    {
        _context.experience.Add(formData);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFormData), new { DocId = formData.docid }, formData);
    }

    // PUT: api/FormData/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormData(int id, experience formData)
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
        var formData = await _context.experience.FindAsync(id);

        if (formData == null)
        {
            return NotFound();
        }

        _context.experience.Remove(formData);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FormDataExists(int id)
    {
        return _context.experience.Any(e => e.docid == id);
    }
}

