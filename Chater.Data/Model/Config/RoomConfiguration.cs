using Chater.Data.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chater.Data.Model.Config
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(r => r.Description)
                .HasMaxLength(512);

            builder.Property(r => r.RoomAvatarUrl)
                .HasMaxLength(500);

            builder.Property(r => r.CreatedAt)
                .HasPrecision(0);

            // Relationships
            builder.HasOne(r => r.CreatedBy)
                .WithMany(u => u.OwnedRooms)
                .HasForeignKey(r => r.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Messages)
                .WithOne(m => m.Room)
                .HasForeignKey(m => m.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 