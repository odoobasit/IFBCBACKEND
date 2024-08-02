using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

[Route("api/[controller]")]
[ApiController]
public class CandidateProfileController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CandidateProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/CandidateProfile
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidateProfile>>> GetAllCandidateProfiles()
    {
        try
        {
            var candidateProfiles = await _context.CandidateProfile.ToListAsync();

            foreach (var profile in candidateProfiles)
            {
                // Example logging
                Console.WriteLine($"docid: {profile.docid}, AgentUserId: {profile.AgentUserId}");
            }

            if (candidateProfiles == null || candidateProfiles.Count == 0)
            {
                return NotFound();
            }

            return Ok(candidateProfiles);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex}");
            throw; // Rethrow the exception to propagate it
        }
    }



    // GET: api/CandidateProfile/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CandidateProfile>> GetCandidateProfileById(int id)
    {
        var candidateProfile = await _context.CandidateProfile.FindAsync(id);

        if (candidateProfile == null)
        {
            return NotFound();
        }

        return Ok(candidateProfile);
    }

    // POST: api/CandidateProfile
    [HttpPost]
    public async Task<ActionResult<CandidateProfile>> CreateCandidateProfile(CandidateProfile candidateProfile)
    {
        _context.CandidateProfile.Add(candidateProfile);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCandidateProfileById), new { id = candidateProfile.docid }, candidateProfile);
    }

    // PUT: api/CandidateProfile/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCandidateProfile(int id, CandidateProfile candidateProfile)
    {
        if (id != candidateProfile.docid)
        {
            return BadRequest();
        }

        _context.Entry(candidateProfile).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CandidateProfileExists(id))
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

    // DELETE: api/CandidateProfile/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCandidateProfile(int id)
    {
        var candidateProfile = await _context.CandidateProfile.FindAsync(id);

        if (candidateProfile == null)
        {
            return NotFound();
        }

        _context.CandidateProfile.Remove(candidateProfile);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CandidateProfileExists(int id)
    {
        return _context.CandidateProfile.Any(e => e.docid == id);
    }
}
