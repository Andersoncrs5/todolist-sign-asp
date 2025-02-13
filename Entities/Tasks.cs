using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApi.Entities;

[Table("tasks")]
public class Tasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    [StringLength(100, ErrorMessage = "Max size is 100")]
    [Required(ErrorMessage = "Field Title is required")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Max size is 500")]
    [Required(ErrorMessage = "Field Description is required")]
    public string Description { get; set; } = string.Empty;

    public bool IsDone { get; set; } = false;

    [Required(ErrorMessage = "Field FkUserId is required")]
    public ulong FkUserId { get; set; }
}
