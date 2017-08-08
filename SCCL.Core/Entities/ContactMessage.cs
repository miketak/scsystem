using System;
using System.ComponentModel.DataAnnotations;

namespace SCCL.Core.Entities
{
    public class ContactMessage
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime DatePosted { get; set; }
    }
}