using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMentorAPI.Models;

namespace FMentorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorWorkingTimesController : ControllerBase
    {
        private readonly FMentorDBContext _context;

        public MentorWorkingTimesController(FMentorDBContext context)
        {
            _context = context;
        }

        // GET: api/MentorWorkingTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentorWorkingTime>>> GetMentorWorkingTimes()
        {
            return await _context.MentorWorkingTimes.ToListAsync();
        }

        // GET: api/MentorWorkingTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MentorWorkingTime>> GetMentorWorkingTime(int id)
        {
            var mentorWorkingTime = await _context.MentorWorkingTimes.FindAsync(id);

            if (mentorWorkingTime == null)
            {
                return NotFound();
            }

            return mentorWorkingTime;
        }

        // PUT: api/MentorWorkingTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentorWorkingTime(int id, MentorWorkingTime mentorWorkingTime)
        {
            if (id != mentorWorkingTime.MentorId)
            {
                return BadRequest();
            }

            _context.Entry(mentorWorkingTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentorWorkingTimeExists(id))
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

        // POST: api/MentorWorkingTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MentorWorkingTime>> PostMentorWorkingTime(MentorWorkingTime mentorWorkingTime)
        {
            _context.MentorWorkingTimes.Add(mentorWorkingTime);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MentorWorkingTimeExists(mentorWorkingTime.MentorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMentorWorkingTime", new { id = mentorWorkingTime.MentorId }, mentorWorkingTime);
        }

        // DELETE: api/MentorWorkingTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentorWorkingTime(int id)
        {
            var mentorWorkingTime = await _context.MentorWorkingTimes.FindAsync(id);
            if (mentorWorkingTime == null)
            {
                return NotFound();
            }

            _context.MentorWorkingTimes.Remove(mentorWorkingTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MentorWorkingTimeExists(int id)
        {
            return _context.MentorWorkingTimes.Any(e => e.MentorId == id);
        }
    }
}
