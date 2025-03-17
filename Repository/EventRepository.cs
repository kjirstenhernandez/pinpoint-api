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
        
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateAsync(Event eventModel)
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public async Task<Event?> DeleteAsync(string id)
        {
            var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.id == id);
            if (eventModel == null)
            {
                return null;
            }
            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<List<Event>> GetAllByUserAsync(string userId)
        {
            return await _context.Events.Where(x => x.userId == userId).ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(string id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event?> UpdateAsync(string id, UpdateEventRequestDto eventDto)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(x => x.id == id);
            if (existingEvent == null){
                return null;
            }

                existingEvent.title = eventDto.title;
                existingEvent.description = eventDto.description;
                existingEvent.address = eventDto.address;
                existingEvent.lat = eventDto.lat;
                existingEvent.lon = eventDto.lon;
                existingEvent.date = eventDto.date;
                existingEvent.time = eventDto.time;
                existingEvent.type = eventDto.type;

                await _context.SaveChangesAsync();
                return existingEvent;
        }
    }
}