using Microsoft.AspNetCore.Identity;
using ProjectManagement.Data.Entities;

namespace ProjectManagement.Data;

public class User : IdentityUser<string>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<Assignment> Assignments { get; set; }

    public ICollection<Project> Projects { get; set; }
}
