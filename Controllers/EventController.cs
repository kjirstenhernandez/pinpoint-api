using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;
        private readonly IUserRepository _userRepo;

        public EventController(IEventRepository eventRepo, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _eventRepo = eventRepo;
        }

// GET METHODS

        // Get all events
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var events = await _eventRepo.GetAllAsync();
            var eventsModel = events.Select(s => s.ToEventDto());
            return Ok(events);
        }
        [HttpGet("user/{id}")]
        public async Task <IActionResult> GetAllByUser([FromRoute] string id)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var events = await _eventRepo.GetAllByUserAsync(id);
            var eventsModel = events.Select(s => s.ToEventDto());
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById([FromRoute] string id)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var theEvent = await _eventRepo.GetByIdAsync(id);

            if (theEvent == null){
                return NotFound();
            }

            return Ok(theEvent.ToEventDto()); 
        }

// ADD METHOD
        [HttpPost("{userId}")]
        public async Task <IActionResult> Create([FromRoute] string userId, [FromBody] CreateEventRequestDto eventDto)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            if (!await _userRepo.UserExists(userId)){
                return BadRequest("User does not exist");
            }
            var eventModel = eventDto.ToEventsFromCreateDTO();
            await _eventRepo.CreateAsync(eventModel);
            return CreatedAtAction(nameof(GetById), new {id = eventModel.id}, eventModel.ToEventDto());
        }

// UPDATE METHOD
        [HttpPut]
        [Route("{id}")]
        public async Task <IActionResult> Update([FromRoute] string id, [FromBody] UpdateEventRequestDto updateDto)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var eventInfo = await _eventRepo.UpdateAsync(id, updateDto);

            if (eventInfo == null)
            {
                return NotFound();
            }

            return Ok(eventInfo.ToEventDto());
        }

//  DELETE METHOD
        [HttpDelete]
        [Route("{id}")]

        public async Task <IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var eventModel = await _eventRepo.DeleteAsync(id);
            if(eventModel == null){
                return NotFound();
            }

            return NoContent();
        }
    }
}