namespace Chater.Data.DTOs;

public class ChatRoomResponseDto
{
  public string Name { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
  public string AvatarUrl { get; set; } = string.Empty;
}