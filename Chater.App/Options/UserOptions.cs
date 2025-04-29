using System.ComponentModel.DataAnnotations;
using Chater.Data.Model.Entities;

public class UserOptions
{
  [Required]
  [Range(0, uint.MaxValue, ErrorMessage = $"{nameof(ChatRoomsLimit)} value out of range.")]
  public uint ChatRoomsLimit { get; set; }
}