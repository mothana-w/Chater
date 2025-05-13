namespace Chater.Data.DTOs;

public class MessageResponseDto 
{
  public string Content { get; set; } = null!;
  public DateTime? UpdatedAt { get; set; }
  public bool IsEdited { get; set; }
  public virtual UserResponseDto Sender { get; set; } = null!;
}