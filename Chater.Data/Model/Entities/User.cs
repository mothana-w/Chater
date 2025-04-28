using System;
using System.Collections.Generic;

namespace Chater.Data.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? ProfileAvatarUrl { get; set; }
        public string? Bio { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public virtual ICollection<Room> OwnedRooms { get; set; } = new List<Room>();
        public virtual ICollection<RoomMember> RoomsMembers { get; set; } = new List<RoomMember>();
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
} 