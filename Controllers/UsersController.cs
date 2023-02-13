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
using FMentorAPI.DTOs.RequestModel;
using FMentorAPI.DTOs.RequestModel.UpdateRequestModel;
using System.ComponentModel.DataAnnotations;

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
            var jobs = await _context.Jobs.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate).ToListAsync();
            var educations = await _context.Educations.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate).ToListAsync();
            user.Jobs = jobs;
            user.Educations = educations;
            return _mapper.Map<UserResponseModel>(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserRequestModel model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            if (model == null)
                return BadRequest("Model is empty!");
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid!");
            if(model.Age != null)
                user.Age = (int) model.Age;
            if(model.Description != null)
                user.Description = model.Description;
            if (model.Photo != null)
                user.Photo = model.Photo;
            if(model.VideoIntroduction != null) user.VideoIntroduction = model.VideoIntroduction;
            user.Name = model.Name;
            try
            {
                var entity = _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<UserResponseModel>(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<UserResponseModel>> SignIn(SignInRequestModel model,[Required] string token)
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
            if (_context.UserTokens.FirstOrDefault(u => u.UserId == user.UserId && u.Token.Equals(token)) == null)
            {
                _context.UserTokens.Add(new UserToken { UserId = user.UserId, Token = token });
                _context.SaveChanges();
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
                    var userSpecialty = _context.UserSpecialties.FirstOrDefault(u => u.UserId == id && u.SpecialtyId == specialty.SpecialtyId);
                    if (specialty != null && userSpecialty == null)
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
                
                var jobs = await _context.Jobs.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate).ToListAsync();
                var educations = await _context.Educations.Where(j => j.UserId == user.UserId).OrderBy(j => j.StartDate).ToListAsync();
                user.Jobs = jobs;
                user.Educations = educations;
                return Ok(_mapper.Map<UserResponseModel>(user));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
