using AutoMapper;
using BookTravel.APi.Dtos;
using BookTravel.APi.Models;
using BookTravel.APi.Sevrices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookTravel.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRole.Admin)]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ITravelerService _travelerService;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;
        public BookingsController(IBookingService bookingService, IMapper mapper
            , ITravelerService travelerService, IAppointmentService appointmentService)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _appointmentService = appointmentService;
            _travelerService = travelerService;
        }
        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateBookingAsync([FromBody] BookingDto Dto)
        {
            var appointment = await _appointmentService.GetAsync(p => p.Id == Dto.AppointmentId && p.NumberTravelers > 0);

            if (await _travelerService.IsTravelerAsync(Dto.UserId) is null ||
                appointment is null)
                return NotFound();

            appointment.NumberTravelers--;
            _appointmentService.Update(appointment);

            await _bookingService.CreateAsync(_mapper.Map<Booking>(Dto));

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetBookingsAsync()
        {
            var bookings = await _bookingService.GetAsync();
            if (bookings is null)
                return NotFound();
            return Ok(bookings);
        }
        [HttpPost("Confirem/{id}")]
        public async Task<IActionResult> ConfiremBookingAsync(long id)
        {
            var Booking = await _bookingService.GetAsync(id);
            if (Booking is null)
                return NotFound();

            _bookingService.ConfiremBooking(Booking);
            return Ok();
        }
    }
}
