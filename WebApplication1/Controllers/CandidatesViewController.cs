using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesViewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CandidatesViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateViewModel>>> GetCandidatesView()
        {
            return await _context.CandidatesView.ToListAsync();
        }

        // GET: api/FormData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateViewModel>> GetFormData(int id)
        {
            var formData = await _context.CandidatesView
                                        .FirstOrDefaultAsync(cv => cv.DocId == id);

            if (formData == null)
            {
                return NotFound();
            }

            return formData;
        }

    }
}
