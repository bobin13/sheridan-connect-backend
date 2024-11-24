using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
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

        // [HttpPost]
        // public IActionResult AddPost([FromBody] Post post)
        // {
        //     try
        //     {
        //         if (post == null)
        //         {
        //             return BadRequest("Invalid post data.");
        //         }

        //         post.TimePosted = DateTime.UtcNow; // Add current time to the post
        //         var collection = dbContext.GetAllPost();
        //         collection.InsertOne(post); // Insert the new post into MongoDB

        //         return Ok("Post added successfully!");
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

 [HttpPost]
public IActionResult AddPost([FromBody] Post post)
{
    try
    {
        if (post == null)
        {
            return BadRequest("Invalid post data.");
        }

        // Generate a new ObjectId if _id is empty
        if (string.IsNullOrEmpty(post._id))
        {
            post._id = ObjectId.GenerateNewId().ToString();
        }

        post.TimePosted = DateTime.UtcNow;
        
        // Initialize lists if they're null
        post.Comments ??= new List<string>();
        post.Likes ??= new List<string>();

        var collection = dbContext.GetAllPost();
        collection.InsertOne(post);

        return Ok(new { message = "Post added successfully!", postId = post._id });
    }
    catch (Exception e)
    {
        Console.WriteLine("Error in AddPost: " + e.ToString());
        return StatusCode(500, new { message = "Internal server error", error = e.Message });
    }
}

    }
}
