using AutoMapper;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.WebAPI.Controllers
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
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserSpecialties(
            [FromQuery] int? specialtyId)
        {
            var result = _context.UserSpecialties.Include(x => x.User).AsQueryable();

            if (specialtyId != null)
            {
                result = result.Where(x => x.SpecialtyId == specialtyId);
            }

            return _mapper.Map<List<UserInfo>>(await result.Where(x => x.User.IsMentor == 1).Select(x => x.User)
                .ToListAsync());
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