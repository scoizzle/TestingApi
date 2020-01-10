using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingApi.Models;

namespace TestingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase {
        private readonly MessageContext _context;

        public ChannelsController(MessageContext context)
        {
            _context = context;
        }

        // GET: api/Channels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetChannels()
        {
            var dictionary = await _context.Messages.ToDictionaryAsync(msg => msg.Channel);

            return dictionary.Keys;
        }
    }
}