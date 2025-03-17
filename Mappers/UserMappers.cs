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
            return new UserDto
            {
                id = userModel.id,
                username = userModel.username,
                firstName = userModel.firstName,
                lastName = userModel.lastName,
                zipcode = userModel.zipcode,
                email = userModel.email,
                phone = userModel.phone,
                events = userModel.Events.Select(c => c.ToEventDto()).ToList()
            };
        }

        public static User ToUsersFromCreateDTO(this CreateUserRequestDto userDto)
        {
            return new User
            {
                username = userDto.username,
                firstName = userDto.firstName,
                lastName = userDto.lastName,
                zipcode = userDto.zipcode,
                email = userDto.email,
                phone = userDto.phone
            };
        }
    }
}