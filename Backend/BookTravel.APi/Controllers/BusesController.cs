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
    public class BusesController : ControllerBase
    {
        private readonly IBusService _busService;
        private readonly IMapper _mapper;
        public BusesController(IBusService busService, IMapper mapper)
        {
            _busService = busService;
            _mapper = mapper;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBusAsync([FromBody]BusDto Dto)
        {
            var Bus = await _busService.CreateAsync(_mapper.Map<Bus>(Dto));

            if (Bus is null)
                return BadRequest();

            return Ok(Bus);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateBusAsync(int id,[FromBody] BusDto Dto)
        {
            var Bus = await _busService.GetAsync(id);
            if (Bus is null || !Bus.IsAvailable)
                return NotFound();
            var UpdatedBus = _busService.Update(_mapper.Map(Dto, Bus));

            if (UpdatedBus is null)
                return BadRequest();

            return Ok(UpdatedBus);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBusAsync(int Id)
        {
            var Bus = await _busService.GetAsync(Id);
            if (Bus is null)
                return NotFound();

            return Ok(Bus);
        }
        [HttpGet]
        public async Task<IActionResult> GetBusesAsync()
        {
            var Buses = await _busService.GetAsync();
            if (Buses.Count() == 0)
                return NotFound();
            return Ok(Buses);
        }
        [HttpGet("Available")]
        public async Task<IActionResult> GetAvailableBusesAsync()
        {
            var Buses = await _busService.GetAvailableBusesAsync();
            if (Buses.Count() == 0)
                return NotFound();
            return Ok(Buses);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBusAsync(int Id)
        {
            var Bus = await _busService.GetAsync(Id);
            if (Bus is null)
                return NotFound();

            var DeltedBus=_busService.Delete(Bus);
            if (DeltedBus is null)
                return BadRequest();

            return Ok(DeltedBus);
        }
    }
}
