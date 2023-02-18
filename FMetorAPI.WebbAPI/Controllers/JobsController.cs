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
    public class JobsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public JobsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobResponseModel>>> GetJobs()
        {
            return _mapper.Map<List<JobResponseModel>>(await _context.Jobs.ToListAsync());
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<JobResponseModel>>> GetJobsByUserId(int id)
        {
            return _mapper.Map<List<JobResponseModel>>(await _context.Jobs.Where(j => j.UserId == id).OrderBy(j => j.StartDate).ToListAsync());
        }
        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobResponseModel>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return _mapper.Map<JobResponseModel>(job);
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, JobRequestModel job)
        {
            var job1 = await _context.Jobs.FindAsync(id);
            if (job1 == null)
            {
                return NotFound();
            }
            if(_context.Users.Find(job.UserId) == null)
                return NotFound("User not found!");
            if (job == null)
            {
                return BadRequest("Job is empty!");
            }
            if (!ModelState.IsValid)
                return BadRequest("Job is not valid!");
            //if (_context.Jobs.Any(j => j.IsCurrent == 1))
            //    return BadRequest("Current job is already exits!");
            if (job.StartDate > DateTime.Now)
                return BadRequest("Start date must be before or equal today!");
            //if (_context.Jobs.Any(j => j.IsCurrent == 1))
            //    return BadRequest("Current job is already exits!");
            if (!job.IsCurrent && job.EndDate == null)
                return BadRequest("Required the end date");
            if (job.StartDate >= job.EndDate)
                return BadRequest("Start date must be before end date!");
            if (job.EndDate > DateTime.Now)
                return BadRequest("End date must be before or equal today!");
            //if (job.StartDate != null && job.StartDate < lastEndDate)
            //    return BadRequest("The new job start date must be after the last job end date");


            if (_context.Jobs.Where(j => j.UserId == j.UserId && j.Role.Equals(job.Role) && j.Company.Equals(job.Company) && j.StartDate == job.StartDate).Count() > 1)
                return BadRequest("The job is already exist!");
            byte isCurrent = job.IsCurrent ? byte.Parse("1") : byte.Parse("0");
            
            job1.Role = job.Role;
            job1.Company = job.Company;
            job1.IsCurrent = isCurrent;
            job1.StartDate = job.StartDate;
            job1.EndDate = job.EndDate;
            try
            {
                var entity = _context.Jobs.Update(job1);
                _context.SaveChanges();
                return Ok(_mapper.Map<JobResponseModel>(entity.Entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobResponseModel>> PostJob(JobRequestModel job)
        {
            //var lastEndDate = _context.Jobs.ToList().OrderByDescending(x => x.EndDate).FirstOrDefault().EndDate;
            if (job == null)
            {
                return BadRequest("Job is empty!");
            }
            if (_context.Users.Find(job.UserId) == null)
                return NotFound("User not found!");
            if (!ModelState.IsValid)
                return BadRequest("Job is not valid!");
            //if (_context.Jobs.Any(j => j.IsCurrent == 1))
            //    return BadRequest("Current job is already exits!");
            if (job.StartDate > DateTime.Now)
                return BadRequest("Start date must be before or equal today!");
            //if (_context.Jobs.Any(j => j.IsCurrent == 1))
            //    return BadRequest("Current job is already exits!");
            if (!job.IsCurrent && job.EndDate == null)
                return BadRequest("Required the end date");
            if (job.StartDate >= job.EndDate)
                return BadRequest("Start date must be before end date!");
            if (job.EndDate > DateTime.Now)
                return BadRequest("End date must be before or equal today!");
            //if (job.StartDate != null && job.StartDate < lastEndDate)
            //    return BadRequest("The new job start date must be after the last job end date");


            if (_context.Jobs.FirstOrDefault(j => j.UserId == j.UserId && j.Role.Equals(job.Role) && j.Company.Equals(job.Company) && j.StartDate == job.StartDate) != null)
                return BadRequest("The job is already exist!");
            byte isCurrent = job.IsCurrent ? byte.Parse("1") : byte.Parse("0");
            Job job1 = new Job { UserId = job.UserId, Company = job.Company, IsCurrent = isCurrent, StartDate = job.StartDate, EndDate = job.EndDate, Role = job.Role };
            try
            {
                var entity = _context.Jobs.Add(job1);
                _context.SaveChanges();
                return CreatedAtAction("GetJob", new { id = job1.JobId }, _mapper.Map<JobResponseModel>(entity.Entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
