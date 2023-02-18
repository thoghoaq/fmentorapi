using AutoMapper;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public UserPermissionsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UserPermissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPermissionResponseModel>>> GetUserPermissions()
        {
            return _mapper.Map<List<UserPermissionResponseModel>>(await _context.UserPermissions.ToListAsync());
        }

        // GET: api/UserPermissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPermissionResponseModel>> GetUserPermission(byte id)
        {
            var userPermission = _mapper.Map<UserPermissionResponseModel>(await _context.UserPermissions.FindAsync(id));

            if (userPermission == null)
            {
                return NotFound();
            }

            return userPermission;
        }

        // PUT: api/UserPermissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPermission(byte id, UserPermissionResponseModel userPermission)
        {
            if (id != userPermission.IsMentor)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<UserPermission>(userPermission)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPermissionExists(id))
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

        // POST: api/UserPermissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPermissionResponseModel>> PostUserPermission(UserPermissionResponseModel userPermission)
        {
            _context.UserPermissions.Add(_mapper.Map<UserPermission>(userPermission));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserPermissionExists(userPermission.IsMentor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserPermission", new { id = userPermission.IsMentor }, userPermission);
        }

        // DELETE: api/UserPermissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPermission(byte id)
        {
            var userPermission = await _context.UserPermissions.FindAsync(id);
            if (userPermission == null)
            {
                return NotFound();
            }

            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPermissionExists(byte id)
        {
            return _context.UserPermissions.Any(e => e.IsMentor == id);
        }
    }
}
