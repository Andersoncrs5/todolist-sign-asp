using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApi.Entities;

[Table("user")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [Required(ErrorMessage = "Field name is required")]
    [StringLength(100, ErrorMessage = "Max size of 100")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Field name is required")]
    [StringLength(150, ErrorMessage = "Max size of 150")]
    [EmailAddress(ErrorMessage = "Email invalied")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Field name is required")]
    [StringLength(50, ErrorMessage = "Max size of 50")]
    public string Password { get; set; } = string.Empty;
}