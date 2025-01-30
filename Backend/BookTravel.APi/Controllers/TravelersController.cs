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
    [Authorize(Roles = AppRole.Admin)]
    public class TravelersController : ControllerBase
    {
        private readonly ITravelerService _travelerService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public TravelersController(ITravelerService travelerService, IAuthService authService, IMapper mapper)
        {
            _travelerService = travelerService;
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTraveler([FromBody] TravelerDto Dto)
        {
            var User = await _authService.GetUserAsync(Dto.Email) ?? await _authService.GetUserAsync(Dto.UserName);
            if (User is not null)
            {
                ModelState.AddModelError("Email", ErrorMessge.EmailExist);
                ModelState.AddModelError("UserName", ErrorMessge.NameExist);
                return BadRequest(ModelState);
            }

            var ErrorMessege = await _travelerService.CreateTravelerAsync(_mapper.Map<ApplicationUser>(Dto), Dto.Password);
            if (!string.IsNullOrEmpty(ErrorMessege))
                return BadRequest(ErrorMessege);

            return Ok();
        }
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> UpdateTraveler([FromBody] TravelerDto Dto, string Id)
        {
            var Traveler = await _travelerService.IsTravelerAsync(Id);
            if (Traveler is null)
                return NotFound();

            var ErrorMessege = await _travelerService.UpdateTravelerAsync(_mapper.Map(Dto, Traveler));
            if (!string.IsNullOrEmpty(ErrorMessege))
                return BadRequest(ErrorMessege);

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> RetrieveTravelers()
        {
            var travelers = await _travelerService.RetrieveTravelersAsync();
            if (travelers.Count() == 0)
                return NotFound();

            return Ok(travelers);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> RetrieveTraveler(string Id)
        {
            var Traveler = await _travelerService.IsTravelerAsync(Id);
            if (Traveler is null)
                return NotFound();

            return Ok(_mapper.Map<TravelerViewDto>(Traveler));
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTraveler(string Id)
        {
            var Traveler = await _travelerService.IsTravelerAsync(Id);
            if (Traveler is null)
                return NotFound();

            var ErrorMessege = await _travelerService.DeleteTravelerAsync(Traveler);
            if (!string.IsNullOrEmpty(ErrorMessege))
                return BadRequest(ErrorMessege);

            return Ok(Traveler.Id);
        }

    }
}
