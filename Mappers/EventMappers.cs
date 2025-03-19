using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Models;

namespace api.Mappers
{
    public static class EventMappers
    {
        public static EventDto ToEventDto(this Event eventModel)
        {
            return new EventDto
            {
                // Shares the same information for an event, minus the longitude and latitude
                id = eventModel.id,
                title = eventModel.title,
                description = eventModel.description,
                address = eventModel.address,
                date = eventModel.date,
                time = eventModel.time,
                type = eventModel.type,
                userId = eventModel.userId

            };
        }
    
        public static Event ToEventsFromCreateDTO(this CreateEventRequestDto eventDto)
        {
            return new Event
            {
                // Event DTO that shares the same information for an event, minus the event id which will be auto-generated.
                title = eventDto.title,
                description = eventDto.description,
                address = eventDto.address,
                lat = eventDto.lat,
                lon = eventDto.lon,
                date = eventDto.date,
                time = eventDto.time,
                type = eventDto.type,
                userId = eventDto.userId
            };
        }
    }
}