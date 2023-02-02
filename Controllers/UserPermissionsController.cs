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
    public class UserPermissionsController : ControllerBase
    {
        private readonly FMentorDBContext _context;

        public UserPermissionsController(FMentorDBContext context)
        {
            _context = context;
        }

        // GET: api/UserPermissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPermission>>> GetUserPermissions()
        {
            return await _context.UserPermissions.ToListAsync();
        }

        // GET: api/UserPermissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPermission>> GetUserPermission(int id)
        {
            var userPermission = await _context.UserPermissions.FindAsync(id);

            if (userPermission == null)
            {
                return NotFound();
            }

            return userPermission;
        }

        // PUT: api/UserPermissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPermission(int id, UserPermission userPermission)
        {
            if (id != userPermission.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userPermission).State = EntityState.Modified;

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
        public async Task<ActionResult<UserPermission>> PostUserPermission(UserPermission userPermission)
        {
            _context.UserPermissions.Add(userPermission);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserPermissionExists(userPermission.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserPermission", new { id = userPermission.UserId }, userPermission);
        }

        // DELETE: api/UserPermissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPermission(int id)
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

        private bool UserPermissionExists(int id)
        {
            return _context.UserPermissions.Any(e => e.UserId == id);
        }
    }
}
