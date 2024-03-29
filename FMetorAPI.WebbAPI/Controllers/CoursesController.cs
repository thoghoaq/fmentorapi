﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using AutoMapper;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.BusinessLogic.DTOs.RequestModel;
using FMentorAPI.BusinessLogic.FCMNotification;
using FMentorAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public CoursesController(FMentorDBContext context, IMapper mapper, INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _notificationService = notificationService;
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
        [HttpGet("/api/courses/is-favorite")]
        public async Task<ActionResult<bool>> CheckIfMenterIsFollowedByMentee([Required] int courseId, [Required] int menteeId)
        {
            return _context.FavoriteCourses.FirstOrDefault(f => f.MenteeId == menteeId && f.CourseId == courseId) != null;

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

        [HttpPost("recommended-courses/{token}")]
        public async Task<ActionResult> RecommendedCourseForMentee(String token, List<int> courseResponseModels)
        {
            var user = await _context.UserTokens.FirstOrDefaultAsync(u => u.Token.Equals(token));
            if (user == null)
                return NotFound("User is not already exist!");

            NotificationRequestModel notificationModel = new NotificationRequestModel
            {
                DeviceId = token,
                IsAndroiodDevice = true,
                Title = "Recommended courses!",
                Body = "Recommended courses!",
                Route = "recommended - " + JsonSerializer.Serialize(courseResponseModels)
            };

            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        [HttpPost("recommended-course")]
        public async Task<ActionResult<CourseResponseModel>> GetRecommendCourse(List<int> ids)
        {
            List<Course> favoriteCourses = new List<Course>();
            foreach (int id in ids)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
                if(course != null)
                    favoriteCourses.Add(course);
            }

            if (favoriteCourses == null)
            {
                return NotFound();
            }

            var courses = new List<CourseResponseModel>();

            foreach (var favoriteCourse in favoriteCourses)
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
                    });
                }
            }

            return courses != null ? Ok(courses) : NotFound();
        }

        [HttpGet("mentor/{id}")]
        public async Task<ActionResult<CourseResponseModel>> GetCoursesByMentor(int id)
        {
            var mentor = await _context.Mentors.FirstOrDefaultAsync(u => u.MentorId == id);
            if (mentor == null)
                return NotFound("Mentor is not already exist!");
            
            var favoriteCourses = await _context.Courses.Where(m => m.MentorId == id).ToListAsync();

            if (favoriteCourses == null)
            {
                return NotFound();
            }

            var courses = new List<CourseResponseModel>();

            foreach (var favoriteCourse in favoriteCourses)
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
                    });
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
