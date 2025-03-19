using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Models;

namespace api.Interfaces
{
    public interface IEventRepository
    // Event repo interface
    {
        Task<List<Event>> GetAllAsync(); // Get list of all events
        Task<List<Event>> GetAllByUserAsync(string userId); // Get all events made by a specified user
        Task<Event?> GetByIdAsync(string id); // Get event by its unique ID

        Task<Event> CreateAsync(Event eventModel); // create a new event
        Task<Event?> UpdateAsync(string id, UpdateEventRequestDto eventDto); // update an existing event
        Task<Event?> DeleteAsync(string id); // Delete an event using the unique ID.
    }
}