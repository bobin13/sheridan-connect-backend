using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using sheridan_connect_backend.DB;
using sheridan_connect_backend.Models;

namespace sheridan_connect_backend.controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly UserService? _userService;

        public UsersController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        MongoDbContext db = new MongoDbContext();

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = db.GetUserCollection().AsQueryable();
                return Ok(users);
            }
            catch (MongoException e)
            {

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message
                );
            }



        }

        [HttpGet("{username}")]
        public IActionResult GetCommunitiesForUser(string username)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("username", username);

                var users = db.GetUserCollection().Find(filter).First();

                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Email parameter is required." });
            }

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(user);
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] string email, [FromBody] User updatedUser)
        {
            try
            {
                var existingUser = await _userService.GetUserByEmailAsync(email);
                if (existingUser == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                await _userService.UpdateUserAsync(email, updatedUser);
                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }


    }
}