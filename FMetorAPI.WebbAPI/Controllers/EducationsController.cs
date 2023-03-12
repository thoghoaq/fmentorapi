using AutoMapper;
using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.BusinessLogic.DTOs.RequestModel;
using FMentorAPI.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public EducationsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationResponseModel>>> GetEducations()
        {
            return _mapper.Map<List<EducationResponseModel>>(await _context.Educations.ToListAsync());
        }

        // GET: api/Educations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationResponseModel>> GetEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return _mapper.Map<EducationResponseModel>(education);
        }

        // PUT: api/Educations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(int id, EducationRequestModel education)
        {
            var education1 = await _context.Educations.FindAsync(id);
            if (education1 == null)
            {
                return NotFound("Not found education");
            }
            if (education == null)
            {
                return BadRequest("Education is empty!");
            }
            if (_context.Users.Find(education.UserId) == null)
                return NotFound("User not found!");
            if (!ModelState.IsValid)
                return BadRequest("Education is not valid!");
            if(education.StartDate > DateTime.Now)
                return BadRequest("Start date must be before or equal today!");
            //if (_context.Jobs.Any(j => j.IsCurrent == 1))
            //    return BadRequest("Current job is already exits!");
            if (!education.IsCurrent && education.EndDate == null)
                return BadRequest("Required the end date");
            if (education.StartDate >= education.EndDate)
                return BadRequest("Start date must be before end date!");
            if (education.EndDate > DateTime.Now)
                return BadRequest("End date must be before or equal today!");
            //if (job.StartDate != null && job.StartDate < lastEndDate)
            //    return BadRequest("The new job start date must be after the last job end date");


            if (_context.Educations.Where(j => j.UserId == j.UserId && j.School.Equals(education.School) && j.Major.Equals(education.Major) && j.StartDate == education.StartDate).Count() > 1)
                return BadRequest("The education is already exist!");
            byte isCurrent = education.IsCurrent ? byte.Parse("1") : byte.Parse("0");
            //Education education1 = new Education { UserId = education.UserId, School = education.School, IsCurrent = isCurrent, StartDate = education.StartDate, EndDate = education.EndDate, Major = education.Major };
            education1.StartDate = education.StartDate;
            education1.IsCurrent = isCurrent;
            education1.EndDate = education.EndDate;
            education1.School = education.School;
            education1.Major = education.Major;
            try
            {
                var entity = _context.Educations.Update(education1);
                _context.SaveChanges();
                return Ok(_mapper.Map<EducationResponseModel>(entity.Entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Educations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EducationResponseModel>> PostEducation(EducationRequestModel education)
        {
            //var lastEndDate = _context.Jobs.ToList().OrderByDescending(x => x.EndDate).FirstOrDefault().EndDate;
            if (education == null)
            {
                return BadRequest("Education is empty!");
            }
            if (_context.Users.Find(education.UserId) == null)
                return NotFound("User not found!");
            if (!ModelState.IsValid)
                return BadRequest("Education is not valid!");
            //if (_context.Jobs.Any(j => j.IsCurrent == 1))
            //    return BadRequest("Current job is already exits!");
            if (education.StartDate > DateTime.Now)
                return BadRequest("Start date must be before or equal today!");
            //if (_context.Jobs.Any(j => j.IsCurrent == 1))
            //    return BadRequest("Current job is already exits!");
            if (!education.IsCurrent && education.EndDate == null)
                return BadRequest("Required the end date");
            if (education.StartDate >= education.EndDate)
                return BadRequest("Start date must be before end date!");
            if (education.EndDate > DateTime.Now)
                return BadRequest("End date must be before or equal today!");
            //if (job.StartDate != null && job.StartDate < lastEndDate)
            //    return BadRequest("The new job start date must be after the last job end date");


            if (_context.Educations.FirstOrDefault(j => j.UserId == j.UserId && j.School.Equals(education.School) && j.Major.Equals(education.Major) && j.StartDate == education.StartDate) != null)
                return BadRequest("The education is already exist!");
            byte isCurrent = education.IsCurrent ? byte.Parse("1") : byte.Parse("0");
            Education education1 = new Education { UserId = education.UserId, School = education.School, IsCurrent = isCurrent, StartDate = education.StartDate, EndDate = education.EndDate, Major = education.Major };
            try
            {
                var entity = _context.Educations.Add(education1);
                _context.SaveChanges();
                return CreatedAtAction("GetEducation", new { id = education1.EducationId }, _mapper.Map<EducationResponseModel>(entity.Entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.EducationId == id);
        }
    }
}
