using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;


[Route("api/[controller]")]
[ApiController]
public class wantsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public wantsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/FormData
    [HttpGet]
    public async Task<ActionResult<IEnumerable<wants>>> GetFormData()


    {
        Console.WriteLine(_context.wants);

        if (_context.wants == null)
        {
            return NotFound();
        }
        return await _context.wants.ToListAsync();
    }

    // GET: api/FormData/5
    [HttpGet("{id}")]
    public async Task<ActionResult<wants>> GetFormData(int id)
    {
        var formData = await _context.wants.FindAsync(id);

        if (formData == null)
        {
            return NotFound();
        }

        return formData;
    }

    // POST: api/FormData
    [HttpPost]
    public async Task<ActionResult<wants>> PostFormData(wants formData)
    {
        _context.wants.Add(formData);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFormData), new { DocId = formData.docid }, formData);
    }

    // PUT: api/FormData/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormData(int id, wants formData)
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
        var formData = await _context.wants.FindAsync(id);

        if (formData == null)
        {
            return NotFound();
        }

        _context.wants.Remove(formData);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FormDataExists(int id)
    {
        return _context.wants.Any(e => e.docid == id);
    }
}

