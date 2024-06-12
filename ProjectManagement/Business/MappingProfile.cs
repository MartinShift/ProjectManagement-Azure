using AutoMapper;
using ProjectManagement.Business.Models;
using ProjectManagement.Data;
using ProjectManagement.Data.Entities;

namespace ProjectManagement.Business;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();
        CreateMap<Assignment, AssignmentDto>();
        CreateMap<AssignmentDto, Assignment>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}
