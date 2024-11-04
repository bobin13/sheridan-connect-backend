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
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {

        MongoDbContext db = new MongoDbContext();

        [HttpGet]
        public async Task<dynamic> Test()
        {
            var filter = Builders<dynamic>.Filter.Eq("username", "TakamuraMamoru");
            var users = db.GetCollection("users").Find(filter).First();
            Console.WriteLine();
            return Ok(users);
        }
    }
}