using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using sheridan_connect_backend.DB;

namespace sheridan_connect_backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsfeedController : ControllerBase
    {
        MongoDbContext db = new MongoDbContext();

        [HttpGet]
        public IActionResult Newsfeed()
        {
            try
            {
                var posts = db.GetAllPost().AsQueryable();
                return Ok(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}