using api.Dtos.Event;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{

    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepo; // Establishing the event repo interface
        private readonly IUserRepository _userRepo;  // Establishing teh user repo interface

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
            if (!ModelState.IsValid){  // Validation 
                return BadRequest(ModelState);
            }

            var events = await _eventRepo.GetAllAsync();  // Accessing the event interface
            var eventsModel = events.Select(s => s.ToEventDto());  // Processing the data into an event DTO
            return Ok(eventsModel);
        }

        //Get all events by one user
        [HttpGet("user/{id}")]
        public async Task <IActionResult> GetAllByUser([FromRoute] string id)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var events = await _eventRepo.GetAllByUserAsync(id); // accessing the event interface
            var eventsModel = events.Select(s => s.ToEventDto()); // Processing the data into an event DTO
            return Ok(eventsModel);
        }
        // Get a brief summary of the event
        [HttpGet("brief/{id}")]
        public async Task <IActionResult> GetBriefAsync([FromRoute] string id)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var events = await _eventRepo.GetByIdAsync(id); // gather the event data from the database

            if (events == null){
                return NotFound();
            }

            return Ok(events.ToBriefEventDto()); // return the event information using the brief DTO. 
        }

        // Get the details of a specific event
        [HttpGet("{id}")]
        public async Task <IActionResult> GetById([FromRoute] string id) // Display event information by the event's ID
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var theEvent = await _eventRepo.GetByIdAsync(id); // accessing the interface

            if (theEvent == null){ 
                return NotFound(); // Return a "Not Found" error if the ID returns no results 
            }

            return Ok(theEvent.ToEventDto());  // Return the information via an Event DTO
        }

// ADD METHOD
        [HttpPost("{userId}")]
        public async Task <IActionResult> Create([FromRoute] string userId, [FromBody] CreateEventRequestDto eventDto)  // Create a new Event from information gathered in the request (taking user ID from route to add to the request)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState); 
            }

            if (!await _userRepo.UserExists(userId)){
                return BadRequest("User does not exist"); // Return error if User doesn't exist in DB
            }
            var eventModel = eventDto.ToEventsFromCreateDTO();  // create a new Event based off the request body
            await _eventRepo.CreateAsync(eventModel); // use the DTO to access the interface and save to the DB
            return CreatedAtAction(nameof(GetById), new {id = eventModel.id}, eventModel.ToEventDto()); // display DTO information
        }

// UPDATE METHOD
        [HttpPut]
        [Route("{id}")]
        public async Task <IActionResult> Update([FromRoute] string id, [FromBody] UpdateEventRequestDto updateDto) // Update Event information using a route parameter and info from the request body. 
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var eventInfo = await _eventRepo.UpdateAsync(id, updateDto);  // Use the update data to update the db through the interface

            if (eventInfo == null)
            {
                return NotFound();
            }

            return Ok(eventInfo.ToEventDto()); // display the updated information via an event DTO
        }

//  DELETE METHOD
        [HttpDelete]
        [Route("{id}")]

        public async Task <IActionResult> Delete([FromRoute] string id)  // Delete an event using the parameter for a string ID.  
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var eventModel = await _eventRepo.DeleteAsync(id);  // access the db through the interface and provide the event id to be deleted
            if(eventModel == null){
                return NotFound();
            }

            return NoContent(); // No content gives a 200 code, showing that the job was completed. 
        }

// Read/Write Event Logs

        // Endpoint that produces a log of the brief on all events in teh database
        [HttpGet("brief/all")]
        public async Task<IActionResult> GetAllBriefsAsync()
        {
            var allEvents = await _eventRepo.GetAllAsync(); // uses the Get All method to retrieve all events in the database

            if (allEvents == null)
            {
                return NotFound(); // if there are no events in teh database, return NotFound
            }

            var briefEvents = allEvents.Select(e => e.ToBriefEventDto()).ToList();

            // Loop through and write all event details to a file
            WriteMultipleEventsToFile(briefEvents);

            return Ok(briefEvents);
        }

        // Creates the text file containing the briefs from all events in the database
        private void WriteMultipleEventsToFile(List<BriefEventDto> events)
        {
            string path = "Logs/EventLogs.txt";

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                foreach (var eventDto in events) // takes each of the briefs and writes it out into the file with Today's Date: Id, title, event date, and event time 
                {
                    writer.WriteLine($"{DateTime.Now}: {eventDto.id}, {eventDto.title}, {eventDto.date}, {eventDto.time}");
                }
            }
        }

        // Reads the logged event briefs into a list
        private List<string> ReadEventLogs()
        {
            string path = "Logs/EventLogs.txt";

            if (!System.IO.File.Exists(path))
                return new List<string>(); // Return empty if file doesn't exist

            List<string> logs = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    logs.Add(line);
                }
            }

            return logs;
        }


    }
}