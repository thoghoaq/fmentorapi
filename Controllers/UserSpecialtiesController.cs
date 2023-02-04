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
    public class UserSpecialtiesController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public UserSpecialtiesController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UserSpecialties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSpecialtyResponseModel>>> GetUserSpecialties()
        {
            return _mapper.Map<List<UserSpecialtyResponseModel>>(await _context.UserSpecialties.ToListAsync());
        }

        // GET: api/UserSpecialties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSpecialtyResponseModel>> GetUserSpecialty(int id)
        {
            var userSpecialty = await _context.UserSpecialties.FindAsync(id);

            if (userSpecialty == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserSpecialtyResponseModel>(userSpecialty);
        }

        // PUT: api/UserSpecialties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSpecialty(int id, UserSpecialty userSpecialty)
        {
            if (id != userSpecialty.UserSpecialtyId)
            {
                return BadRequest();
            }

            _context.Entry(userSpecialty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSpecialtyExists(id))
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

        // POST: api/UserSpecialties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSpecialty>> PostUserSpecialty(UserSpecialty userSpecialty)
        {
            _context.UserSpecialties.Add(userSpecialty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSpecialty", new { id = userSpecialty.UserSpecialtyId }, userSpecialty);
        }

        // DELETE: api/UserSpecialties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSpecialty(int id)
        {
            var userSpecialty = await _context.UserSpecialties.FindAsync(id);
            if (userSpecialty == null)
            {
                return NotFound();
            }

            _context.UserSpecialties.Remove(userSpecialty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSpecialtyExists(int id)
        {
            return _context.UserSpecialties.Any(e => e.UserSpecialtyId == id);
        }
    }
}
