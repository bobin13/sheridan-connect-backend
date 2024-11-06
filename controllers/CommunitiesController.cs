using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using sheridan_connect_backend.DB;

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
                return Ok(communities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}