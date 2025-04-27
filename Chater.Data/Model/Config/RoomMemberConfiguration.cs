using Chater.Data.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chater.Data.Model.Config
{
    public class RoomMemberConfiguration : IEntityTypeConfiguration<RoomMember>
    {
        public void Configure(EntityTypeBuilder<RoomMember> builder)
        {
            builder.ToTable("RoomsMembers");

            builder.HasKey(rm => new {rm.RoomId, rm.MemeberId});

            builder.Property(rm => rm.JoinedAt)
                .HasPrecision(0);

            // Unique constraint for user-room combination
            builder.HasIndex(rm => new { rm.RoomId, rm.MemeberId })
                .IsUnique();
        }
    }
} 