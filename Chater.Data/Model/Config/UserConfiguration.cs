using System.Security.Cryptography.X509Certificates;
using Chater.Data.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chater.Data.Model.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.PasswordHash)
                .IsRequired();


            builder.Property(u => u.ProfileAvatarUrl)
                .HasMaxLength(500);

            builder.Property(u => u.Bio)
                .HasMaxLength(512);

            builder.Property(u => u.CreatedAt)
                .HasPrecision(0);

            // Relationships
            builder.HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // many to many
            builder.HasMany(u => u.Rooms)
                    .WithMany(r => r.Members)
                    .UsingEntity<RoomMember>(
                        l => l.HasOne(rm => rm.Room).WithMany(r => r.RoomsMembers).HasForeignKey(rm => rm.RoomId),
                        r => r.HasOne(rm => rm.Member).WithMany(u => u.RoomsMembers).HasForeignKey(rm => rm.MemeberId)
                    );
        }
    }
} 