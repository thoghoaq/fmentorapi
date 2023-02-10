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
    public class SpecialtiesController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public SpecialtiesController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Specialties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialtyResponseModel>>> GetSpecialties()
        {
            List<Specialty> specialties = await _context.Specialties.ToListAsync();
            List<UserSpecialty> userSpecialties = await _context.UserSpecialties.ToListAsync();
            return (from specialty in specialties
                    join userSpecialty in userSpecialties
                    on specialty.SpecialtyId equals userSpecialty.SpecialtyId
                    into g
                    select
                    new SpecialtyResponseModel
                    {
                        SpecialtyId = specialty.SpecialtyId,
                        Name = specialty.Name,
                        NumberMentor = g.Count(),
                        Picture = specialty.Picture
                    })
                .OrderByDescending(x => x.NumberMentor)
                .ToList();
        }

        [HttpGet("top3")]
        public async Task<ActionResult<IEnumerable<SpecialtyResponseModel>>> GetTop3Specialties()
        {
            List<Specialty> specialties = await _context.Specialties.ToListAsync();
            List<UserSpecialty> userSpecialties = await _context.UserSpecialties.ToListAsync();
            return (from specialty in specialties
                    join userSpecialty in userSpecialties
                    on specialty.SpecialtyId equals userSpecialty.SpecialtyId
                    into g
                    select 
                    new SpecialtyResponseModel
                    {
                        SpecialtyId = specialty.SpecialtyId,
                        Name = specialty.Name,
                        NumberMentor = g.Count(),
                        Picture = specialty.Picture
                    })
                .OrderByDescending(x => x.NumberMentor).Take(3)
                .ToList();
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<SpecialtyResponseModel>>> GetSpecialtiesByUserId(int id)
        {
            if (_context.Users.Find(id) == null)
                return NotFound("User is not already exist!");
            List<Specialty> specialties = new List<Specialty>();
            List<UserSpecialty> userSpecialties = await _context.UserSpecialties.Where(u => u.UserId == id).ToListAsync();
            foreach (var userSpecialty in userSpecialties)
            {
                var specialty = _context.Specialties.Find(userSpecialty.SpecialtyId);
                if (specialty != null)
                    specialties.Add(specialty);
            }
            return (from specialty in specialties
                    join userSpecialty in userSpecialties
                    on specialty.SpecialtyId equals userSpecialty.SpecialtyId
                    into g
                    select
                    new SpecialtyResponseModel
                    {
                        SpecialtyId = specialty.SpecialtyId,
                        Name = specialty.Name,
                        NumberMentor = g.Count(),
                        Picture = specialty.Picture
                    })
                .OrderByDescending(x => x.NumberMentor)
                .ToList();
        }
        // GET: api/Specialties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialtyResponseModel>> GetSpecialty(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);

            if (specialty == null)
            {
                return NotFound();
            }

            return _mapper.Map<SpecialtyResponseModel>(specialty);
        }

        // PUT: api/Specialties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialty(int id, Specialty specialty)
        {
            if (id != specialty.SpecialtyId)
            {
                return BadRequest();
            }

            _context.Entry(specialty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialtyExists(id))
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

        // POST: api/Specialties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialty>> PostSpecialty(Specialty specialty)
        {
            _context.Specialties.Add(specialty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialty", new { id = specialty.SpecialtyId }, specialty);
        }

        // DELETE: api/Specialties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty == null)
            {
                return NotFound();
            }

            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialtyExists(int id)
        {
            return _context.Specialties.Any(e => e.SpecialtyId == id);
        }
    }
}
