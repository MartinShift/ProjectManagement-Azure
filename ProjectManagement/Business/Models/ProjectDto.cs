using ProjectManagement.Data;

namespace ProjectManagement.Business.Models;

public class ProjectDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<User> Members { get; set; }
}
