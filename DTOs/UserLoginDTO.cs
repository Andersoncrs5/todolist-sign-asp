using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.DTOs;

public class UserLoginDTO
{
    [Required(ErrorMessage = "Field name is required")]
    [StringLength(150, ErrorMessage = "Max size of 150")]
    [EmailAddress(ErrorMessage = "Email invalied")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Field name is required")]
    [StringLength(50, ErrorMessage = "Max size of 50")]
    public string Password { get; set; } = string.Empty;
}
