using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FMentorAPI.Models;
using AutoMapper;
using FMentorAPI.DTOs;
using System.Diagnostics.Metrics;

namespace FMentorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorAvailabilitiesController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public MentorAvailabilitiesController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/MentorAvailabilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentorAvailabilityResponseModel>>> GetMentorAvailabilities()
        {
            return _mapper.Map<List<MentorAvailabilityResponseModel>>(await _context.MentorAvailabilities.ToListAsync());
        }

        // GET: api/MentorAvailabilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MentorAvailabilityResponseModel>> GetMentorAvailability(int id)
        {
            var mentorAvailability = await _context.MentorAvailabilities.FindAsync(id);

            if (mentorAvailability == null)
            {
                return NotFound();
            }

            return _mapper.Map<MentorAvailabilityResponseModel>(mentorAvailability);
        }

        // PUT: api/MentorAvailabilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentorAvailability(int id, MentorAvailability mentorAvailability)
        {
            if (id != mentorAvailability.MentorId)
            {
                return BadRequest();
            }

            _context.Entry(mentorAvailability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentorAvailabilityExists(id))
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

        // POST: api/MentorAvailabilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MentorAvailability>> PostMentorAvailability(MentorAvailability mentorAvailability)
        {
            _context.MentorAvailabilities.Add(mentorAvailability);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MentorAvailabilityExists(mentorAvailability.MentorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMentorAvailability", new { id = mentorAvailability.MentorId }, mentorAvailability);
        }

        // DELETE: api/MentorAvailabilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentorAvailability(int id)
        {
            var mentorAvailability = await _context.MentorAvailabilities.FindAsync(id);
            if (mentorAvailability == null)
            {
                return NotFound();
            }

            _context.MentorAvailabilities.Remove(mentorAvailability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MentorAvailabilityExists(int id)
        {
            return _context.MentorAvailabilities.Any(e => e.MentorId == id);
        }
    }
}
