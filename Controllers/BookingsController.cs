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

namespace FMentorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly FMentorDBContext _context;
        private readonly IMapper _mapper;

        public BookingsController(FMentorDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponseModel>>> GetBookings()
        {
            return _mapper.Map<List<BookingResponseModel>> (await _context.Bookings.Include(m => m.Mentor).ToListAsync());
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponseModel>> GetBooking(int id)
        {
            var booking = _context.Bookings.Include(m => m.Mentor).Where(u => u.BookingId == id).FirstOrDefault();

            if (booking == null)
            {
                return NotFound();
            }

            return _mapper.Map<BookingResponseModel>(booking);
        }

        [HttpGet("mentee/{id}")]
        public async Task<ActionResult<IEnumerable<BookingResponseModel>>> GetBookingsByMentee(int id)
        {
            var bookings = await _context.Bookings.Include(m => m.Mentor).Where(u => u.MenteeId == id).ToListAsync();
            foreach(Booking booking in bookings)
            {
                var mentor = booking.Mentor;
                if (mentor != null)
                {
                    var user = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).FirstOrDefault();
                    if (user == null)
                    {
                        return NotFound();
                    }
                    mentor.User = user;
                }
            }
            return _mapper.Map<List<BookingResponseModel>>(bookings);
        }

        [HttpGet("mentor/{id}")]
        public async Task<ActionResult<IEnumerable<BookingResponseModel>>> GetBookingsByMentor(int id)
        {
            var bookings = await _context.Bookings.Include(m => m.Mentee).Where(u => u.MentorId == id).ToListAsync();
            foreach (Booking booking in bookings)
            {
                var mentor = booking.Mentee;
                if (mentor != null)
                {
                    var user = _context.Users.Where(u => u.UserId == mentor.UserId).Include(j => j.Jobs).Include(e => e.Educations).FirstOrDefault();
                    if (user == null)
                    {
                        return NotFound();
                    }
                    mentor.User = user;
                }
            }
            return _mapper.Map<List<BookingResponseModel>>(bookings);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
