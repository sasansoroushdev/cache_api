using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreCaching.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AspNetCoreCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICacheService _catcheService;

        public TestController(ICacheService catcheService)
        {
            _catcheService = catcheService;
        }

        [HttpGet]
        public string Get()
        {
            return "Please enter cacheKey at the end of the URL";
        }

        [HttpGet("{cacheKey}")]
        public async Task<List<string>> Get(string cacheKey)
        {
            return await _catcheService.GetList(cacheKey);
        }

        [HttpPost("{cacheKey}")]
        public async Task<IActionResult> Post([FromBody] List<string> stringList, string cacheKey)
        {
            var result = await _catcheService.SetList(stringList, cacheKey);
            if (result) return Ok(cacheKey);
            else return StatusCode(500);
        }          
    }
}