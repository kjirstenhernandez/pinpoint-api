using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Models;

namespace api.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<List<Event>> GetAllByUserAsync(string userId);
        Task<Event?> GetByIdAsync(string id);

        Task<Event> CreateAsync(Event eventModel);
        Task<Event?> UpdateAsync(string id, UpdateEventRequestDto eventDto);
        Task<Event?> DeleteAsync(string id);
    }
}