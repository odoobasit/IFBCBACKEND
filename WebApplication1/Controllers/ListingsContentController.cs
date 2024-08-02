using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class ListingsContentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ListingsContentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/YourTable
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListingsContent>>> GetYourTableNames()
    {
        return await _context.ListingsContent.ToListAsync();
    }

    // GET: api/YourTable/5
    [HttpGet("{Name}")]
    public async Task<ActionResult<ListingsContent>> GetYourTableName(string Name)
    {
        var Listings = await _context.ListingsContent.FindAsync(Name);

        if (Listings == null)
        {
            return NotFound();
        }

        return Listings;
    }
}
