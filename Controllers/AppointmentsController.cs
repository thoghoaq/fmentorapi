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
using FMentorAPI.DTOs.RequestModel;
using FMentorAPI.Extensions.ZoomAPI;

namespace FMentorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;
        private readonly IZoomExtension _zoomExtension;

        public AppointmentsController(FMentorDBContext context, IMapper mapper, IZoomExtension zoomExtension)
        {
            _context = context;
            _mapper = mapper;
            _zoomExtension = zoomExtension;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentResponseModel>>> GetAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
            foreach(var appointment in appointments)
            {
                var mentor = _context.Mentors.Find(appointment.MentorId);
                if (mentor == null)
                    return NotFound();
                var mentee = _context.Mentees.Find(appointment.MenteeId);
                if (mentee == null)
                    return NotFound();
                var user = _context.Users.Where(u => u.UserId == mentee.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                var user1 = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                if (user1 == null || user == null)
                {
                    return NotFound();
                }
                mentor.User = user1;
                mentee.User = user;
                appointment.Mentor = mentor;
                appointment.Mentee = mentee;
            }
            return _mapper.Map<List<AppointmentResponseModel>>(appointments);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentResponseModel>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }
            var mentor = _context.Mentors.Find(appointment.MentorId);
            if (mentor == null)
                return NotFound();
            var mentee = _context.Mentees.Find(appointment.MenteeId);
            if (mentee == null)
                return NotFound();
            var user = _context.Users.Where(u => u.UserId == mentee.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
            var user1 = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
            if (user1 == null || user == null)
            {
                return NotFound();
            }
            mentor.User = user1;
            mentee.User = user;
            appointment.Mentor = mentor;
            appointment.Mentee = mentee;

            return _mapper.Map<AppointmentResponseModel>(appointment);
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        [HttpGet("mentee/{id}")]
        public async Task<ActionResult<IEnumerable<AppointmentResponseModel>>> GetAppointmentsByMentee(int id)
        {
            var appointments = await _context.Appointments.Include(m => m.Mentor).Where(u => u.MenteeId == id).ToListAsync();
            foreach (var appointment in appointments)
            {
                var mentor = _context.Mentors.Find(appointment.MentorId);
                if (mentor == null)
                    return NotFound();
                var mentee = _context.Mentees.Find(appointment.MenteeId);
                if (mentee == null)
                    return NotFound();
                var user = _context.Users.Where(u => u.UserId == mentee.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                var user1 = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                if (user1 == null || user == null)
                {
                    return NotFound();
                }
                mentor.User = user1;
                mentee.User = user;
                appointment.Mentor = mentor;
                appointment.Mentee = mentee;
            }
            return _mapper.Map<List<AppointmentResponseModel>>(appointments);
        }

        [HttpGet("mentor/{id}")]
        public async Task<ActionResult<IEnumerable<AppointmentResponseModel>>> GetAppointmentsByMentor(int id)
        {
            var appointments = await _context.Appointments.Include(m => m.Mentee).Where(u => u.MentorId == id).ToListAsync();
            foreach (var appointment in appointments)
            {
                var mentor = _context.Mentors.Find(appointment.MentorId);
                if (mentor == null)
                    return NotFound();
                var mentee = _context.Mentees.Find(appointment.MenteeId);
                if (mentee == null)
                    return NotFound();
                var user = _context.Users.Where(u => u.UserId == mentee.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                var user1 = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                if (user1 == null || user == null)
                {
                    return NotFound();
                }
                mentor.User = user1;
                mentee.User = user;
                appointment.Mentor = mentor;
                appointment.Mentee = mentee;
            }
            return _mapper.Map<List<AppointmentResponseModel>>(appointments);
        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(int bookingId, string? note)
        {
            Booking? booking = _context.Bookings.Where(b => b.BookingId == bookingId && b.Status == "Scheduled").FirstOrDefault();
            if (booking == null)
            {
                return BadRequest();
            }
            ZoomLinkModel zoomLink = await _zoomExtension.CreateMeetingAsync();
            Appointment newAppointment = new()
            {
                GoogleMeetLink = zoomLink.Url,
                Password = zoomLink.Password,
                EndTime = booking.StartTime.AddMinutes(booking.Duration),
                IsReviewed = false,
                Duration = booking.Duration,
                MenteeId = booking.MenteeId,
                MentorId = booking.MentorId,
                Status = "Accepted",
                StartTime = booking.StartTime,
                Note = note,
            };
            _context.Appointments.Add(_mapper.Map<Appointment>(newAppointment));
            var status = await _context.SaveChangesAsync();
            if (status == 1)
            {
                booking.Status = "Accepted";
                _context.Bookings.Update(booking);
                var updateStatus = await _context.SaveChangesAsync();
                if (updateStatus == 0)
                {
                    throw new Exception("UPDATE_BOOKING_STATUS_FAIL");
                }
            } else
            {
                throw new Exception("CREATE_APPOINTMENT_FAIL");
            }

            return CreatedAtAction("GetAppointment", new { id = newAppointment.AppointmentId }, newAppointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
