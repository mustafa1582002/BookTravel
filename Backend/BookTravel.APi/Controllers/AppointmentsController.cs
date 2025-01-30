using AutoMapper;
using BookTravel.APi.Dtos;
using BookTravel.APi.Models;
using BookTravel.APi.Sevrices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookTravel.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =AppRole.Admin)]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IBusService _busService;
        private readonly IMapper _mapper;
        public AppointmentsController(IAppointmentService appointmentService, IMapper mapper, IBusService busService)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
            _busService = busService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAppointmentsAsync()
        {
            var Appointments = await _appointmentService.GetAsync(b => b.Buses);
            if (Appointments.Count() == 0)
                return NotFound();
            return Ok(Appointments);
        }
        [HttpGet("Available")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailableAppointmentAsync()
        {
            var Appointments = await _appointmentService.GetAvailableAppointmentAsync();
            if (Appointments.Count() == 0)
                return NotFound();
            return Ok(Appointments);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAppointmentsAsync([FromBody] AppointmentDto Dto)
        {
            var Appointment = await PopulateBusesAsync(_mapper.Map<Appointment>(Dto), Dto.Buses);

            if (Appointment == null)
                return NotFound(ErrorMessge.NotFoundBuses);

            var AppointmentCreated = await _appointmentService.CreateAsync(Appointment);
            if (AppointmentCreated is null)
                return BadRequest();

            return Ok(AppointmentCreated);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAppointmentAsync(long Id)
        {
            var Appointment = await _appointmentService.GetAsync(i => i.Id == Id, o => o.Buses);
            if (Appointment is null)
                return NotFound();

            return Ok(Appointment);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAppointmentAsync(long Id, AppointmentDto Dto)
        {
            Appointment? Appointment = await _appointmentService.GetAsync(i => i.Id == Id, o => o.Buses);
            if (Appointment is null)
                return NotFound();

            Appointment = await PopulateBusesAsync(_mapper.Map(Dto, Appointment), Dto.Buses);

            if (Appointment is null)
                return NotFound(ErrorMessge.NotFoundBuses);

            return Ok(_appointmentService.Update(Appointment));
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAppointmentAsync(long Id)
        {

            Appointment? Appointment = await _appointmentService.GetAsync(i => i.Id == Id, o => o.Buses);
            if (Appointment is null)
                return NotFound();

            _busService.EnableBuses(Appointment.Buses.ToList());

            return Ok(_appointmentService.Delete(Appointment));
        }

        private async Task<Appointment?> PopulateBusesAsync(Appointment Appointment, IList<int> Buses)
        {
            Bus? bus;
            _busService.EnableBuses(Appointment.Buses.ToList());
            Appointment.Buses = new List<Bus>();
            Appointment.NumberTravelers = 0;
            foreach (var busid in Buses)
            {
                bus = await _busService.GetAsync(busid);

                if (bus is null || !bus.IsAvailable)
                    return null;

                bus.IsAvailable = false;
                Appointment.Buses.Add(bus);
                Appointment.NumberTravelers += bus.Capacity;
            }
            return Appointment;
        }


    }
}
