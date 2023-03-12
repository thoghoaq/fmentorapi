using AutoMapper;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public RankingsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Rankings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RankingResponseModel>>> GetRankings()
        {
            return _mapper.Map<List<RankingResponseModel>>(await _context.Rankings.ToListAsync());
        }

        // GET: api/Rankings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RankingResponseModel>> GetRanking(int id)
        {
            var ranking = await _context.Rankings.FindAsync(id);

            if (ranking == null)
            {
                return NotFound();
            }

            return _mapper.Map<RankingResponseModel>(ranking);
        }

        // PUT: api/Rankings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRanking(int id, Ranking ranking)
        {
            if (id != ranking.MentorId)
            {
                return BadRequest();
            }

            _context.Entry(ranking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RankingExists(id))
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

        // POST: api/Rankings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ranking>> PostRanking(Ranking ranking)
        {
            _context.Rankings.Add(ranking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RankingExists(ranking.MentorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRanking", new { id = ranking.MentorId }, ranking);
        }

        // DELETE: api/Rankings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRanking(int id)
        {
            var ranking = await _context.Rankings.FindAsync(id);
            if (ranking == null)
            {
                return NotFound();
            }

            _context.Rankings.Remove(ranking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RankingExists(int id)
        {
            return _context.Rankings.Any(e => e.MentorId == id);
        }
    }
}
