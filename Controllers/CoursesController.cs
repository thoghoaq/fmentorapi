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
    public class CoursesController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public CoursesController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResponseModel>>> GetCourses()
        {
            return _mapper.Map<List<CourseResponseModel>>(await _context.Courses.ToListAsync());
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResponseModel>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return _mapper.Map<CourseResponseModel>(course);
        }
        [HttpGet("favorite/{id}")]
        public async Task<ActionResult<CourseResponseModel>> GetFavoriteCourseByMentee(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return NotFound();
            var mentee = _context.Mentees.Include(m => m.User).FirstOrDefault(m => m.UserId == user.UserId);
            if (mentee == null)
                return NotFound();
            var favoriteCourses = await _context.FavoriteCourses.Where(m => m.MenteeId == mentee.MenteeId).ToListAsync();

            if (favoriteCourses == null)
            {
                return NotFound();
            }

            var courses = new List<CourseResponseModel>();

            foreach(var favoriteCourse in favoriteCourses)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == favoriteCourse.CourseId);
                if (course != null)
                {
                    courses.Add(new CourseResponseModel
                    {
                        CourseId = course.CourseId,
                        Description = course.Description,
                        Instructor = course.Instructor,
                        Link = course.Link,
                        MentorId = course.MentorId,
                        Platform = course.Platform,
                        Photo = course.Photo,
                        Title = course.Title
                    }) ;
                }
            }

            return courses != null ? Ok(courses) : NotFound();
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
