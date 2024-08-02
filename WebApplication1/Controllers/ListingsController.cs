using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ListingsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ListingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/YourTable
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Listings>>> GetYourTableNames()
    {
        try
        {
            var listings = await _context.Listings.ToListAsync();

            // Check if listings is null or empty
            if (listings == null || listings.Count == 0)
            {
                return NotFound("No listings found.");
            }

            return listings;
        }
        catch (Exception ex)
        {
            // Log the exception (implement logging as needed)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET: api/YourTable/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Listings>> GetYourTableName(int id)
    {
        var Listings = await _context.Listings.FindAsync(id);

        if (Listings == null)
        {
            return NotFound();
        }

        return Listings;
    }
}
