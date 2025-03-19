using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;


namespace api.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto // A user DTO instance minus the lat/lon variables
            {
                id = userModel.id,
                username = userModel.username,
                firstName = userModel.firstName,
                lastName = userModel.lastName,
                zipcode = userModel.zipcode,
                email = userModel.email,
                phone = userModel.phone,
            };
        }

        public static UserDto ToUserDetailDto(this User userModel){
            return new UserDto // returns a DTO that includes the events created by the user
            {
                id = userModel.id,
                username = userModel.username,
                firstName = userModel.firstName,
                lastName = userModel.lastName,
                zipcode = userModel.zipcode,
                email = userModel.email,
                phone = userModel.phone,
                Events = userModel.Events.Select(c => c.ToEventDto()).ToList()
            };
        }

        public static User ToUsersFromCreateDTO(this CreateUserRequestDto userDto)
        {
            return new User // A user DTO minus the ID, which will be auto-generated.
            {
                username = userDto.username,
                firstName = userDto.firstName,
                lastName = userDto.lastName,
                zipcode = userDto.zipcode,
                email = userDto.email,
                phone = userDto.phone
                //lat/lon will be gathered by zipcode API in Sprint #2
            };
        }
    }
}