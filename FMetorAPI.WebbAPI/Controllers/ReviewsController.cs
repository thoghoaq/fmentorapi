using AutoMapper;
using AutoMapper.QueryableExtensions;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public ReviewsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResponseModel>>> GetReviews()
        {
            return _mapper.Map<List<ReviewResponseModel>>(await _context.Reviews.ToListAsync());
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseModel>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReviewResponseModel>(review);
        }
     
        [HttpGet("reviewee/{revieweeId}")]
        public async Task<ActionResult<List<ReviewResponseModel>>> GetReviewsByReviewee(int revieweeId)
        {
            var reviews = await _context.Reviews
                .Include(x=>x.Reviewer)
                .Where(x=>x.RevieweeId==revieweeId)
                .ProjectTo<ReviewResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return reviews;
        }

        
        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReviewResponseModel>> PostReview(ReviewResponseModel review)
        {
            if (_context.Users.Find(review.RevieweeId) == null)
                return NotFound("Mentee not found!");
            if (_context.Users.Find(review.ReviewerId) == null)
                return NotFound("Mentor not found!");
            if (_context.Appointments.Find(review.AppointmentId) == null)
                return NotFound("Appointment not found!");
            _context.Reviews.Add(new Review { Rating = review.Rating, Comment = review.Comment, AppointmentId = review.AppointmentId,
            RevieweeId = review.RevieweeId, ReviewerId = review.ReviewerId });
            var appointment = _context.Appointments.Find(review.AppointmentId);
            appointment.IsReviewed = true;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.ReviewId }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
