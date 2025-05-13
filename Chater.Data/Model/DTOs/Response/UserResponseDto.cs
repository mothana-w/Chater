namespace Chater.Data.DTOs;

public class UserResponseDto 
{
  public string Username { get; set; } = null!;
  public string? ProfileAvatarUrl { get; set; }
  public string? Bio { get; set; } = string.Empty;
} 