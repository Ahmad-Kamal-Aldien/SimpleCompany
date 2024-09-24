using AutoMapper;
using Compalny.R.PL.ViewModels;
using Company.R.DAL.Models;

namespace Compalny.R.PL.Mapping.Departments
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

        }
    }
}
