﻿using System;
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
using FMentorAPI.DTOs.RequestModel;

namespace FMentorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public UsersController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetUsers()
        {
            return _mapper.Map<List<UserResponseModel>>(await _context.Users.ToListAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseModel>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserResponseModel>(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<UserResponseModel>> SignIn(SignInRequestModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var user = _context.Users.Where(u => u.Email == model.Email && u.Password == model.Password)
                .Include(m => m.Mentees)
                .Include(m => m.Mentors)
                .Include(m => m.Jobs)
                .Include(m => m.Educations)
                .Include(m => m.Wallet)
                .Include(m => m.Payments)
                .Include(m => m.ReviewReviewees)
                .Include(m => m.ReviewReviewers)
                .Include(m => m.UserSpecialties)
                .Include(m => m.IsMentorNavigation)
                .FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserResponseModel>(user));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("add-specialties/{id}")]
        public async Task<ActionResult<bool>> SignUp([FromBody] List<String> specialtiesName, int id)
        {
            if (specialtiesName == null)
            {
                return BadRequest("List is null!");
            }
            if (_context.Users.Find(id) == null)
                return BadRequest("Id is not exist!");
            if (specialtiesName.Count == 0)
                return Ok(true);
            try
            {
                foreach (var name in specialtiesName)
            {
                
                    var specialty = await _context.Specialties.FirstOrDefaultAsync(s => s.Name.Equals(name));
                    if (specialty != null && _context.UserSpecialties.FirstOrDefault(u => u.UserId == id && u.UserSpecialtyId == specialty.SpecialtyId) == null)
                    {
                        _context.UserSpecialties.Add(new UserSpecialty { SpecialtyId = specialty.SpecialtyId, UserId = id });
                        _context.SaveChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error");
            }

            return Ok(true);
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<UserResponseModel>> SignUp(SignUpRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is empty!");
            }
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid!");
            if(!model.Password.Equals(model.ConfirmPassword))
                return BadRequest("Password is not equal Confirm Password!");
            if (_context.Users.FirstOrDefault(u => u.Email== model.Email) != null)
                return BadRequest("The email address is already exist!");
            User user = new User { Email = model.Email, Name = model.Name, Password = model.Password, Description = " ", IsMentor = 0, Photo = " ", VideoIntroduction = " " };
            try
            {
                var entity = _context.Users.Add(user);
                _context.SaveChanges();
                Mentee mentee = new Mentee { UserId = user.UserId };
                _context.Mentees.Add(mentee);
                _context.SaveChanges();

                var userResponse = _context.Users.Where(u => u.Email == entity.Entity.Email && u.Password == entity.Entity.Password)
                    .Include(m => m.Mentees)
                    .Include(m => m.Mentors)
                    .Include(m => m.Jobs)
                    .Include(m => m.Educations)
                    .Include(m => m.Wallet)
                    .Include(m => m.Payments)
                    .Include(m => m.ReviewReviewees)
                    .Include(m => m.ReviewReviewers)
                    .Include(m => m.UserSpecialties)
                    .Include(m => m.IsMentorNavigation)
                    .FirstOrDefault();

                return Ok(_mapper.Map<UserResponseModel>(user));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
