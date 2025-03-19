using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        
        public EventRepository(ApplicationDbContext context) // Linking with the database
        {
            _context = context;
        }

        public async Task<Event> CreateAsync(Event eventModel)  // Creating a new event in the DB
        {
            await _context.Events.AddAsync(eventModel); // New event is pending
            await _context.SaveChangesAsync(); // New event is saved to DB. 
            return eventModel;
        }

        public async Task<Event?> DeleteAsync(string id)  // Deleting an event in teh DB
        {
            var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.id == id);  // locale the event to be deleted.
            if (eventModel == null)
            {
                return null;
            }
            _context.Events.Remove(eventModel); // prepare teh event to be deleted from the database
            await _context.SaveChangesAsync(); // Save the deletion changes
            return eventModel;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();  // Create a list of events
        }

        public async Task<List<Event>> GetAllByUserAsync(string userId) // Get a list of events created by the specified user
        {
            return await _context.Events.Where(x => x.userId == userId).ToListAsync(); 
        }

        public async Task<Event?> GetByIdAsync(string id) // Get event details by the event's ID
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event?> UpdateAsync(string id, UpdateEventRequestDto eventDto) // Update information on an event based on info from teh provided DTO
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(x => x.id == id);
            if (existingEvent == null){
                return null;
            }
                // take each column of the event pulled from teh DB, and update it with the information provided in the request body.  
                existingEvent.title = eventDto.title;
                existingEvent.description = eventDto.description;
                existingEvent.address = eventDto.address;
                existingEvent.lat = eventDto.lat;
                existingEvent.lon = eventDto.lon;
                existingEvent.date = eventDto.date;
                existingEvent.time = eventDto.time;
                existingEvent.type = eventDto.type;

                await _context.SaveChangesAsync(); // save changes to DB
                return existingEvent; 
        }
    }
}