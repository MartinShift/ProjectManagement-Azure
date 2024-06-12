using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Data.Entities;

public class Assignment
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    public string Status { get; set; }

    public string ProjectId { get; set; }

    public Project Project { get; set; }

    public string AssignedToUserId { get; set; }

    public User AssignedToUser { get; set; }
}
