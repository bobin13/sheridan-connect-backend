using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sheridan_connect_backend.DB;

namespace sheridan_connect_backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            var result = MongoDbContext.GetConnectionString();
            //Console.WriteLine("This is string " + result);
            return Ok(result);
        }
    }
}