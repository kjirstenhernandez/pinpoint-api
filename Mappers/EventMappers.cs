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
                id = eventModel.id,
                title = eventModel.title,
                description = eventModel.description,
                address = eventModel.address,
                lat = eventModel.lat,
                lon = eventModel.lon,
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