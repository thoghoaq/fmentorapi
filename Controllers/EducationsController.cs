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
    public class EducationsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public EducationsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationResponseModel>>> GetEducations()
        {
            return _mapper.Map<List<EducationResponseModel>>(await _context.Educations.ToListAsync());
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationResponseModel>> GetEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return _mapper.Map<EducationResponseModel>(education);
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(int id, Education education)
        {
            if (id != education.EducationId)
            {
                return BadRequest();
            }

            _context.Entry(education).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationExists(id))
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

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Education>> PostEducation(Education education)
        {
            _context.Educations.Add(education);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducation", new { id = education.EducationId }, education);
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.EducationId == id);
        }
    }
}
