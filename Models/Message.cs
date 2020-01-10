using System;

namespace TestingApi.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Channel { get; set; }

        public string Sender { get; set; }

        public string Content { get; set; }
    }
}