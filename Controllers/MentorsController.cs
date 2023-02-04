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
    public class MentorsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public MentorsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Mentors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentorResponseModel>>> GetMentors()
        {
            return _mapper.Map<List<MentorResponseModel>>(await _context.Mentors.ToListAsync());
        }

        // GET: api/Mentors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MentorResponseModel>> GetMentor(int id)
        {
            var mentor = await _context.Mentors.FindAsync(id);

            if (mentor == null)
            {
                return NotFound();
            }

            return _mapper.Map<MentorResponseModel>(mentor);
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
