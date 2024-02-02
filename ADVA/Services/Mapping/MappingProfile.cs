using ADVA.Entities;
using ADVA.Models;
using AutoMapper;

namespace ADVA.Services.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeModel, Employee>();
            CreateMap<DepartmentModel, Department>();

        }

    }
}
