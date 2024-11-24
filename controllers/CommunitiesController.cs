using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using sheridan_connect_backend.DB;
using sheridan_connect_backend.Models;

namespace sheridan_connect_backend.controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CommunitiesController : ControllerBase
    {

        MongoDbContext db = new MongoDbContext();

        [HttpGet("id/{id}")]
        public IActionResult GetAllUserCommunities(ObjectId id)
        {
            try
            {
                //getting the user from recieved id
                var filter = Builders<User>.Filter.Eq("_id", id);
                var user = db.GetCollection<User>("users").Find(filter).FirstOrDefault();
                if (user == null)
                    return BadRequest("User not found!");

                //adding user joined communites to a list of objectIds    
                List<string> list = [];
                foreach (var community in user.JoinedCommunities)
                {
                    list.Add(community.CommunityId);
                }

                //matching ids in the list with communites
                var communities = db.GetCollection<Community>("communities")
                .AsQueryable()
                .Where(i => list.Contains(i._id))
                .ToList();

                return Ok(communities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{searchQuery}")]
        public IActionResult GetCommunities(string searchQuery)
        {
            try
            {
                var filter = Builders<Community>.Filter.Eq("name", new Regex(searchQuery, RegexOptions.IgnoreCase));
                var communities = db.GetCollection<Community>("communities").Find(filter).ToList();
                //var communities = db.GetCollection<Community>("communities").AsQueryable();

                return Ok(communities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddCommunityUser([FromBody] Community community, string id)
        {
            try
            {
                if (community == null)
                    return BadRequest("Invalid Input");

                if (await db.AddCommunity(community, id))
                    return Ok("Community Added!");

                return BadRequest("Error Occured while trying to add community!");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}