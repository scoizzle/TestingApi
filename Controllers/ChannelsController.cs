using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingApi.Models;

namespace TestingApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase {
        private readonly MessageContext _message_context;

        public ChannelsController(MessageContext messageContext)
        {
            _message_context = messageContext;
        }

        // GET: Channels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetChannels()
        {
            var list = await _message_context.Messages
                            .Select(msg => msg.Channel)
                            .Distinct()
                            .ToListAsync();

            return Ok(list);
        }

        // GET: Channels/{name}/Messages
        [HttpGet("{channelName}/Messages")]
        public async Task<ActionResult<IEnumerable<string>>> GetChannelMessages(string channelName)
        {
            var list = await _message_context.Messages
                            .Where(msg => msg.Channel == channelName)
                            .ToListAsync();
                            
            if (list.Count == 0)
                return NotFound();

            return Ok(list);
        }


        // GET: Channels/{name}/Messages/After/{messageId}
        [HttpGet("{channelName}/Messages/After/{messageId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetChannelMessagesAfter(string channelName, Guid messageId)
        {
            var list = await _message_context.Messages
                            .Where(msg => msg.Channel == channelName)
                            .ToListAsync();

            var filtered = list.SkipWhile(msg => msg.Id != messageId).Skip(1);

            return Ok(filtered);
        }

        // POST: Channels/{name}/Messages
        [HttpPost("{channelName}/Messages")]
        public async Task<ActionResult<IEnumerable<string>>> PostChannelMessage(string channelName, Message message)
        {
            if (channelName != message.Channel)
                return BadRequest();
                
            _message_context.Messages.Add(message);
            await _message_context.SaveChangesAsync();

            return Ok();
        }
    }
}