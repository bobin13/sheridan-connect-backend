using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{id}")]
        public IActionResult GetAllUserCommunities(ObjectId id)
        {
            try
            {
                var communities = db.GetAllUserCommunities(id);
                return Ok(communities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllCommunities()
        {
            try
            {
                var communities = db.GetAllCommunities().AsQueryable();

                // foreach (var community in communities)
                // {
                //     community.idString = community._id.ToString();
                //     Console.WriteLine(community._id.ToString());
                // }
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