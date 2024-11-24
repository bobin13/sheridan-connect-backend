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

        MongoDbContext db = new MongoDbContext();

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = db.GetCollection<User>("users").AsQueryable();
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

                var users = db.GetCollection<User>("users").Find(filter).FirstOrDefault();

                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}