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
using System.ComponentModel.DataAnnotations;

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
        private FavoriteCourse GetFavoriteCourse(int courseId, int menteeId)
        {
            return _context.FavoriteCourses.FirstOrDefault(c => c.MenteeId == menteeId && c.CourseId == courseId);
        }


        [HttpPost("/favorite_course")]
        public async Task<ActionResult<FavoriteCourseResponseModel>> FavoriteCourse([Required] int courseId, [Required] int menteeId)
        {
            if (_context.Courses.FirstOrDefault(c => c.CourseId == courseId) == null)
                return NotFound();
            if (_context.Mentees.FirstOrDefault(c => c.MenteeId == menteeId) == null)
                return NotFound();
            var favoriteCourse = GetFavoriteCourse(courseId, menteeId);

            if (favoriteCourse == null)
            {
                favoriteCourse = new FavoriteCourse { CourseId = courseId, MenteeId = menteeId };
                _context.FavoriteCourses.Add(favoriteCourse);
                _context.SaveChanges();
                return new FavoriteCourseResponseModel { CourseId = menteeId, MenteeId = menteeId, IsFavorite = true };
            }
            else return new FavoriteCourseResponseModel { CourseId = menteeId, MenteeId = menteeId, IsFavorite = true };
            return new FavoriteCourseResponseModel { CourseId = menteeId, MenteeId = menteeId, IsFavorite = false };
        }

        [HttpPost("/unfavorite_course")]
        public async Task<ActionResult<FavoriteCourseResponseModel>> UnFavoriteCourse([Required] int courseId, [Required] int menteeId)
        {
            if (_context.Courses.FirstOrDefault(c => c.CourseId == courseId) == null)
                return NotFound();
            if (_context.Mentees.FirstOrDefault(c => c.MenteeId == menteeId) == null)
                return NotFound();
            var favoriteCourse = GetFavoriteCourse(courseId, menteeId);

            if (favoriteCourse != null)
            {
                _context.FavoriteCourses.Remove(favoriteCourse);
                _context.SaveChanges();
                return new FavoriteCourseResponseModel { CourseId = menteeId, MenteeId = menteeId, IsFavorite = false };

            }
            else return new FavoriteCourseResponseModel { CourseId = menteeId, MenteeId = menteeId, IsFavorite = false };

            return new FavoriteCourseResponseModel { CourseId = menteeId, MenteeId = menteeId, IsFavorite = true };
        }
        private FollowedMentor GetFollowedMentor(int mentorId, int menteeId)
        {
            return _context.FollowedMentors.FirstOrDefault(c => c.MenteeId == menteeId && c.MentorId == mentorId);
        }


        [HttpPost("/followed_mentor")]
        public async Task<ActionResult<FollowMentorResponseModel>> FollowedMentor([Required] int mentorId, [Required] int menteeId)
        {
            if (_context.Mentors.FirstOrDefault(c => c.MentorId == mentorId) == null)
                return NotFound();
            if (_context.Mentees.FirstOrDefault(c => c.MenteeId == menteeId) == null)
                return NotFound();
            FollowedMentor followedMenter = GetFollowedMentor(mentorId, menteeId);

            if (followedMenter == null)
            {
                followedMenter = new FollowedMentor { MenteeId = menteeId, MentorId = mentorId };
                _context.FollowedMentors.Add(followedMenter);
                _context.SaveChanges();
                return new FollowMentorResponseModel { MentorId = menteeId, MenteeId = menteeId, IsFollow = true };
            }
            else return new FollowMentorResponseModel { MentorId = menteeId, MenteeId = menteeId, IsFollow = true };
            return new FollowMentorResponseModel { MentorId = menteeId, MenteeId = menteeId, IsFollow = false };
        }

        [HttpPost("/unfollowed_mentor")]
        public async Task<ActionResult<FollowMentorResponseModel>> UnFollowedMentor([Required] int mentorId, [Required] int menteeId)
        {
            if (_context.Mentors.FirstOrDefault(c => c.MentorId == mentorId) == null)
                return NotFound();
            if (_context.Mentees.FirstOrDefault(c => c.MenteeId == menteeId) == null)
                return NotFound();
            FollowedMentor followedMenter = GetFollowedMentor(mentorId, menteeId);

            if (followedMenter != null)
            {
                _context.FollowedMentors.Remove(followedMenter);
                _context.SaveChanges();
                return new FollowMentorResponseModel { MentorId = menteeId, MenteeId = menteeId, IsFollow = false };
                
            } else return new FollowMentorResponseModel { MentorId = menteeId, MenteeId = menteeId, IsFollow = false };

            return new FollowMentorResponseModel { MentorId = menteeId, MenteeId = menteeId, IsFollow = true };
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
            return _context.Mentees.FirstOrDefault(c => c.MenteeId == id) != null;
        }
    }
}
