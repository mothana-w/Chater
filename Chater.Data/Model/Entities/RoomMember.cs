using System;

namespace Chater.Data.Model.Entities
{
    public class RoomMember
    {
        public int RoomId { get; set; }
        public int MemeberId { get; set; }
        public DateTime JoinedAt { get; set; }

        // Navigation properties
        public virtual Room Room { get; set; } = null!;
        public virtual User Member { get; set; } = null!;
    }
} 