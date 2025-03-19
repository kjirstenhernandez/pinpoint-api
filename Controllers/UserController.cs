using api.Data;
using api.Dtos.User;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


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
        public async Task<IActionResult> GetAll() // Display the information for all users
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var users = await _userRepo.GetAllAsync(); // access interface to connect to DB
            var userDto = users.Select(s => s.ToUserDto()); // create a new DTO using the returned info
            return Ok(userDto);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id) // Get User info by ID using route parameters
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await _userRepo.GetByIdAsync(id); //access the interface to connect to DB
            if (user == null) {
                return NotFound(); // return "Not Found" if no user by that ID is found in the database.
            }

            return Ok(user.ToUserDetailDto());  // return the information via DTO

        }
//  ADD METHOD
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var userModel = userDto.ToUsersFromCreateDTO(); // use DTO to create a new User instance
            await _userRepo.CreateAsync(userModel);  // save the new user to the DB
            return CreatedAtAction(nameof(GetById), new { id = userModel.id }, userModel.ToUserDto()); // Display the information for the user that was created

        }

//  UPDATE METHOD
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequestDto updateDto) // Uses Route Parameters to locate the user and update their information based on info from teh body
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var userInfo = await _userRepo.UpdateAsync(id, updateDto); // access the interface to find and update the user

            if(userInfo == null){
                return NotFound(); // if the user can't be found, return Not Found response
            }

            return Ok(userInfo.ToUserDto());  // Upon update, create a new DTO and display the information. 
        }

//  DELETE METHOD

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] string id) // User route parameters to find and delete a user
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var userModel = await _userRepo.DeleteAsync(id); // access the interface to find and delete the user
            if(userModel == null){
                return NotFound(); // if the user can't be located with the given id, returns a Not Found status. 
            }

            return NoContent(); // No content status returned to indicate a successful deletion. 

        }
    }
}//