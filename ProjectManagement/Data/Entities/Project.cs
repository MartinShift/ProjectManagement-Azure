using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Data.Entities;

public class Project
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public ICollection<Assignment> Assignments { get; set; }

    public ICollection<User> Members { get; set; }
}
