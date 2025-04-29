using System.Text.RegularExpressions;

using Chater.App.Services;
using Chater.Data.DTOs;
using Chater.Data.Mappings;
using Chater.Data.Model.Entities;
using Chater.Data.Repository;
using Microsoft.Extensions.Options;

public class ChatRoomService
(
  IBaseRepository<Room> _roomRepo
  , IServiceResultFactory _resFactory
  , ILogger<ChatRoomService> _logger
  , IOptions<UserOptions> _userOpts
) : IChatRoomService
{
  public async Task<ServiceResult> CreateRoom(int uid, ChatRoomRequestDto dto)
  {
    _logger.LogInformation("Started chat room creation.");

    dto.Name = dto.Name.Trim().Normalize();
    dto.Description = dto.Description.Trim().Normalize();
    
    if (_roomRepo.Where(r => r.CreatedById == uid).Count() == _userOpts.Value.ChatRoomsLimit )
      return _resFactory.Failure("User hit chat rooms limit", StatusCodes.Status409Conflict);

    var checkRoomNameResult = CheckRoomName(dto.Name);
    if (! checkRoomNameResult.IsSuccess)
      return checkRoomNameResult; 

    if (await _roomRepo.GetSingleAsync(r => r.Name == dto.Name) is not null)
      return _resFactory.Failure("Chat room name already used", StatusCodes.Status409Conflict);

    var room = dto.MapToRoom();
    room.CreatedById = uid;
    await _roomRepo.AddAsync(room);

    _logger.LogInformation("Finished chat room creation.");
    return _resFactory.Success();
  }
  private ServiceResult CheckRoomName(string name){
    int nameMaxLengthInDb = 128;

    if (string.IsNullOrEmpty(name))
      return _resFactory.Failure($"chat room name can't be empty", StatusCodes.Status400BadRequest);

    if (name.Length >= nameMaxLengthInDb)
      return _resFactory.Failure($"chat room name can't be more than {nameMaxLengthInDb}", StatusCodes.Status400BadRequest);

    var pattern = @"^[\p{L}\p{M}\p{N} _-]+$"; // regex pattern that allows all characters from all languages, numbers, hyphens, underscors and whitspaces.
    if (! Regex.IsMatch(name, pattern))
      return _resFactory.Failure($"chat room name has invalid format", StatusCodes.Status400BadRequest);

    return _resFactory.Success();
  }
}
