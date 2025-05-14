using Chater.Data.DTOs;

namespace Chater.App.Services;

public interface IMessageService
{
    Task<ServiceResult<IEnumerable<MessageResponseDto>>> GetAll(int uid, string roomName);
}