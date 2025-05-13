using Chater.Data.DTOs;
using Chater.Data.Model.Entities;
using Npgsql.Replication;

namespace Chater.Data.Mappings;

public static class DomainToDtoMapper
{
    public static UserResponseDto MapToDto(this User user){
        return new(){
            Username = user.Username,
            ProfileAvatarUrl = user.ProfileAvatarUrl,
            Bio = user.Bio
        };
    }

    public static MessageResponseDto MapToDto(this Message message){
        return new(){
            Content = message.Content,
            UpdatedAt = message.UpdatedAt,
            IsEdited = message.IsEdited,
            Sender = message.Sender.MapToDto()
        };
    }
}