using AutoMapper;
using Compalny.R.PL.ViewModels;
using Company.R.DAL.Models;

namespace Compalny.R.PL.Mapping.Departments
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }
    }
}
