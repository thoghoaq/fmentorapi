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

namespace FMentorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenteesController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public MenteesController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Mentees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenteeResponseModel>>> GetMentees()
        {
            return _mapper.Map<List<MenteeResponseModel>>(await _context.Mentees.ToListAsync());
        }

        // GET: api/Mentees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenteeResponseModel>> GetMentee(int id)
        {
            var mentee = await _context.Mentees.FindAsync(id);

            if (mentee == null)
            {
                return NotFound();
            }

            return _mapper.Map<MenteeResponseModel>(mentee);
        }

        // PUT: api/Mentees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentee(int id, Mentee mentee)
        {
            if (id != mentee.MenteeId)
            {
                return BadRequest();
            }

            _context.Entry(mentee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenteeExists(id))
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

        // POST: api/Mentees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mentee>> PostMentee(Mentee mentee)
        {
            _context.Mentees.Add(mentee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMentee", new { id = mentee.MenteeId }, mentee);
        }

        // DELETE: api/Mentees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentee(int id)
        {
            var mentee = await _context.Mentees.FindAsync(id);
            if (mentee == null)
            {
                return NotFound();
            }

            _context.Mentees.Remove(mentee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenteeExists(int id)
        {
            return _context.Mentees.Any(e => e.MenteeId == id);
        }
    }
}
