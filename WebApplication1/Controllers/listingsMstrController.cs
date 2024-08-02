using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class listingsMstrController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public listingsMstrController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/YourTable
    [HttpGet]
    public async Task<ActionResult<IEnumerable<listingsMstr>>> GetYourTableNames()
    {
        try
        {
            var listingsMstr = await _context.listingsMstr.ToListAsync();

            // Check if listings is null or empty
            if (listingsMstr == null || listingsMstr.Count == 0)
            {
                return NotFound("No listings found.");
            }

            return listingsMstr;
        }
        catch (Exception ex)
        {
            // Log the exception (implement logging as needed)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET: api/YourTable/5
    [HttpGet("{id}")]
    public async Task<ActionResult<listingsMstr>> GetYourTableName(int id)
    {
        var listingsMstr = await _context.listingsMstr.FindAsync(id);

        if (listingsMstr == null)
        {
            return NotFound();
        }

        return listingsMstr;
    }
}
