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
        public async Task<ActionResult<IEnumerable<MentorResponseModel2>>> GetMentors()
        {
            var mentors = await _context.Mentors.Include(u => u.User).ToListAsync();
            foreach (Mentor mentor in mentors)
            {
                var user = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }
                mentor.User = user;
            }
            return _mapper.Map<List<MentorResponseModel2>>(await _context.Mentors.Include(u => u.User).ToListAsync());
        }
        [HttpGet("/api/mentors/followed/{id}")]
        public async Task<ActionResult<IEnumerable<MentorResponseModel>>> GetFollowedMentorsByMenteeId(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return NotFound();
            var mentee = _context.Mentees.Include(m => m.User).FirstOrDefault(m => m.UserId == user.UserId);
            if (mentee == null)
                return NotFound();
            var followedMentors = await _context.FollowedMentors.Where(m => m.MenteeId == mentee.MenteeId).ToListAsync();

            if (followedMentors == null)
            {
                return NotFound();
            }

            var mentors = new List<MentorResponseModel2>();

            foreach (var followedMentor in followedMentors)
            {
                var mentor = await _context.Mentors.FirstOrDefaultAsync(c => c.MentorId == followedMentor.MentorId);
                if (mentor != null)
                {
                    mentors.Add(new MentorResponseModel2
                    {
                        MentorId = mentor.MentorId,
                        Availability = mentor.Availability,
                        HourlyRate = mentor.HourlyRate,
                        Specialty = mentor.Specialty,
                        UserId = mentor.UserId
                    });
                }
            }
            return mentors != null ? Ok(mentors) : NotFound();
        }

        [HttpGet("/api/mentors/specialty/{id}")]
        public async Task<ActionResult<IEnumerable<MentorResponseModel>>> GetFollowedMentorsBySpecialtyId(int id)
        {
            if (_context.Specialties.FirstOrDefault(m => m.SpecialtyId == id) == null)
                return NotFound();
            var userSpecialties = await _context.UserSpecialties.Where(m => m.SpecialtyId == id).ToListAsync();

            if (userSpecialties == null)
            {
                return NotFound();
            }

            var mentors = new List<MentorResponseModel2>();

            foreach (var userSpecialty in userSpecialties)
            {
                var mentor = await _context.Mentors.FirstOrDefaultAsync(c => c.UserId == userSpecialty.UserId);
                if (mentor != null)
                {
                    mentors.Add(new MentorResponseModel2
                    {
                        MentorId = mentor.MentorId,
                        Availability = mentor.Availability,
                        HourlyRate = mentor.HourlyRate,
                        Specialty = mentor.Specialty,
                        UserId = mentor.UserId
                    });
                }
            }
            return mentors != null ? Ok(mentors) : NotFound();
        }

        // GET: api/Mentors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MentorResponseModel2>> GetMentor(int id)
        {
            var mentor = _context.Mentors.Include(u => u.User).Where(m => m.MentorId == id).FirstOrDefault();
            MentorResponseModel2 mentorResponse = null;
            if (mentor != null)
            {
                var user = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                int numberFollowers = _context.FollowedMentors.Where(f => f.MentorId == id).Count();
                int numberMentees = 0;
                List<Course> courses = _context.Courses.Where(c => c.MentorId == id).ToList();
                List<FavoriteCourse> favoriteCourses = _context.FavoriteCourses.ToList();
                numberMentees = (from course in courses
                 join favorite in favoriteCourses
                 on course.CourseId equals favorite.CourseId
                 into g
                 select new { NumberMentee = g.Count() }).Count();
                
                if (user == null)
                {
                    return NotFound();
                }
                mentor.User = user;
                mentorResponse = _mapper.Map<MentorResponseModel2>(mentor);
                mentorResponse.NumberMentee = numberMentees;
                mentorResponse.NumberFollower = numberFollowers;
            }
            if (mentor == null)
            {
                return NotFound();
            }

            return mentorResponse;
        }

        [HttpGet("/api/mentors/user/{id}")]
        public async Task<ActionResult<MentorResponseModel>> GetMentorByUserId(int id)
        {
            var mentor = _context.Mentors.Include(u => u.User).Where(m => m.UserId == id).FirstOrDefault();
            MentorResponseModel mentorResponse = null;
            if (mentor != null)
            {
                var user = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                int numberFollowers = _context.FollowedMentors.Where(f => f.MentorId == id).Count();
                int numberMentees = 0;
                List<Course> courses = _context.Courses.Where(c => c.MentorId == id).ToList();
                List<FavoriteCourse> favoriteCourses = _context.FavoriteCourses.ToList();
                numberMentees = (from course in courses
                                 join favorite in favoriteCourses
                                 on course.CourseId equals favorite.CourseId
                                 into g
                                 select new { NumberMentee = g.Count() }).Count();

                if (user == null)
                {
                    return NotFound();
                }
                mentor.User = user;
                mentorResponse = _mapper.Map<MentorResponseModel>(mentor);
                mentorResponse.NumberMentee = numberMentees;
                mentorResponse.NumberFollower = numberFollowers;
            }
            if (mentor == null)
            {
                return NotFound();
            }

            return mentorResponse;
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
