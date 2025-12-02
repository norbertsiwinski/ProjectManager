using AutoMapper;
using ProjectManager.Domain.Projects;

namespace ProjectManager.Application.Projects.Dtos;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectResponse>()
            .ForCtorParam(
                nameof(ProjectResponse.Name),
                opt => opt.MapFrom(src => src.Name.Value));
    }
}