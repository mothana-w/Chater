using System;
using System.Collections.Generic;

namespace Chater.Data.Model.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int SenderId { get; set; }
        public int? RoomId { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties
        public virtual User Sender { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
} 