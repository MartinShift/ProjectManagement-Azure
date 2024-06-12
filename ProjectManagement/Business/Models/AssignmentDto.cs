namespace ProjectManagement.Business.Models;

public class AssignmentDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public string ProjectId { get; set; }
    public string AssignedToUserId { get; set; }
}
