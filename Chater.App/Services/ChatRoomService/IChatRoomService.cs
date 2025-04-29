using Chater.App.Services;
using Chater.Data.DTOs;

public interface IChatRoomService
{
  Task<ServiceResult> CreateRoom(int uid, ChatRoomRequestDto dto);
}