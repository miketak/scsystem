using System;

namespace SCCL.Core.Entities
{
    public class ContactMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }

        public DateTime DatePosted { get; set; }
    }
}