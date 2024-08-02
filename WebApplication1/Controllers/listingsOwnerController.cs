using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class listingsOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public listingsOwnerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/YourTable
    [HttpGet]
    public async Task<ActionResult<IEnumerable<listingsOwner>>> GetYourTableNames()
    {
        try
        {
            var listingsOwner = await _context.listingsOwner.ToListAsync();

            // Check if listings is null or empty
            if (listingsOwner == null || listingsOwner.Count == 0)
            {
                return NotFound("No listings found.");
            }

            return listingsOwner;
        }
        catch (Exception ex)
        {
            // Log the exception (implement logging as needed)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET: api/YourTable/5
    [HttpGet("{id}")]
    public async Task<ActionResult<listingsOwner>> GetYourTableName(int id)
    {
        var listingsOwner = await _context.listingsOwner.FindAsync(id);

        if (listingsOwner == null)
        {
            return NotFound();
        }

        return listingsOwner;
    }
}
