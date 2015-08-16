using System;

namespace TodoApp.Web.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public int ParticipantId { get; set; }
    }
}