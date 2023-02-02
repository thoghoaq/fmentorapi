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
    public class MentorsController : ControllerBase
    {
        private readonly FMentorDBContext _context;

        public MentorsController(FMentorDBContext context)
        {
            _context = context;
        }

        // GET: api/Mentors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mentor>>> GetMentors()
        {
            return await _context.Mentors.ToListAsync();
        }

        // GET: api/Mentors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mentor>> GetMentor(int id)
        {
            var mentor = await _context.Mentors.FindAsync(id);

            if (mentor == null)
            {
                return NotFound();
            }

            return mentor;
        }

        // PUT: api/Mentors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentor(int id, Mentor mentor)
        {
            if (id != mentor.MentorId)
            {
                return BadRequest();
            }

            _context.Entry(mentor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentorExists(id))
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

        // POST: api/Mentors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mentor>> PostMentor(Mentor mentor)
        {
            _context.Mentors.Add(mentor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMentor", new { id = mentor.MentorId }, mentor);
        }

        // DELETE: api/Mentors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return NotFound();
            }

            _context.Mentors.Remove(mentor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MentorExists(int id)
        {
            return _context.Mentors.Any(e => e.MentorId == id);
        }
    }
}
