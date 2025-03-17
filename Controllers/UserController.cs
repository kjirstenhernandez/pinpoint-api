using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepo;
        public UserController(ApplicationDbContext context, IUserRepository userRepo )
        {
            _userRepo = userRepo;
            _context = context;
        }
        
//  GET METHODS 
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var users = await _userRepo.GetAllAsync();
            var userDto = users.Select(s => s.ToUserDto());
            return Ok(users);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await _userRepo.GetByIdAsync(id);
            Console.WriteLine(user);
            if (user == null) {
                return NotFound();
            }

            return Ok(user.ToUserDto());

        }
//  ADD METHOD
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var userModel = userDto.ToUsersFromCreateDTO();
            await _userRepo.CreateAsync(userModel);
            return CreatedAtAction(nameof(GetById), new { id = userModel.id }, userModel.ToUserDto());

        }

//  UPDATE METHOD
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequestDto updateDto)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var userInfo = await _userRepo.UpdateAsync(id, updateDto);

            if(userInfo == null){
                return NotFound();
            }

            return Ok(userInfo.ToUserDto());
        }

//  DELETE METHOD

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var userModel = await _userRepo.DeleteAsync(id);
            if(userModel == null){
                return NotFound();
            }

            return NoContent();

        }
    }
}//