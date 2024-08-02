using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ListingsViewController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ListingsViewController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/YourTable
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListingsView>>> GetYourTableNames()
    {
        return await _context.ListingsView.ToListAsync();
    }

    // GET: api/YourTable/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ListingsView>> GetYourTableName(int id)
    {
        var Listings = await _context.ListingsView.FindAsync(id);

        if (Listings == null)
        {
            return NotFound();
        }

        return Listings;
    }
}
