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
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessagesController(MessageContext context)
        {
            _context = context;
        }


        // GET: api/Messages/{id}/
        [HttpGet]
        public ActionResult<Message> GetMessageById(Guid id)
        {
            var message = _context.Messages.Single(msg => msg.Id == id);

            if (message == null) {
                return NotFound();
            }

            return Ok(message);
        }

        // GET: api/Messages/{channel}/
        [HttpGet("{channel}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByChannel(string channel)
        {
            var messages = await _context.Messages.Where(msg => msg.Channel == channel).ToListAsync();

            if (messages == null) 
            {
                return NotFound();
            }

            return Ok(messages);
        }

        [HttpPost("")]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToRoute(new 
            { 
                controller = "Messages", 
                action = "GetMessagesByChannel", 
                channel = message.Channel
            });
        }
    }
}
