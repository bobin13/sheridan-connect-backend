// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using MongoDB.Driver;
// using sheridan_connect_backend.DB;
// using sheridan_connect_backend.Models;

// namespace sheridan_connect_backend.controllers
// {
//     [ApiController]
//     [Route("api/v1/[controller]")]
//     public class NewsfeedController : ControllerBase
//     {
//         MongoDbContext db = new MongoDbContext();

//         [HttpGet]
//         public IActionResult Newsfeed()
//         {
//             try
//             {
//                 var posts = db.GetAllPost().AsQueryable();
//                 return Ok(posts);
//             }
//             catch (Exception e)
//             {
//                 return BadRequest(e.Message);
//             }
//         }

//         public async Task<bool> AddPost(Post newPost)
//         {
//             try
//             {
//                 var client = GetMongoClient();
//                 var db = client.GetDatabase(this.dbName);
//                 await db.GetCollection<Post>("posts").InsertOneAsync(newPost);
//                 return true;
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e.Message);
//                 return false;
//             }
//         }
//     }
// }

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using sheridan_connect_backend.DB;
using sheridan_connect_backend.Models;

namespace sheridan_connect_backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewsfeedController : ControllerBase
    {
        private readonly MongoDbContext dbContext;

        public NewsfeedController()
        {
            dbContext = new MongoDbContext(); // Instantiate MongoDbContext
        }

        [HttpGet]
        public IActionResult Newsfeed()
        {
            try
            {
                // Get the collection of posts
                var posts = dbContext.GetAllPost().AsQueryable();
                return Ok(posts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddPost([FromBody] Post post)
        {
            try
            {
                if (post == null)
                {
                    return BadRequest("Invalid post data.");
                }

                post.TimePosted = DateTime.UtcNow; // Add current time to the post
                var collection = dbContext.GetAllPost();
                collection.InsertOne(post); // Insert the new post into MongoDB

                return Ok("Post added successfully!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
