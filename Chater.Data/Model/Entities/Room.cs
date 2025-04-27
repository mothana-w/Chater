using System;
using System.Collections.Generic;

namespace Chater.Data.Model.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string? RoomAvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedById { get; set; }

        // Navigation properties
        public virtual User CreatedBy { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<RoomMember> RoomsMembers { get; set; } = new List<RoomMember>();
        public virtual ICollection<User> Members { get; set; } = new List<User>();
    }
} 