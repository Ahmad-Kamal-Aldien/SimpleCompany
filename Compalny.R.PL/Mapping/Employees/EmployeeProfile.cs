using AutoMapper;
using Company.R.DAL.Models;

namespace Compalny.R.PL.Mapping.Employees
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeProfile, Employee>().ReverseMap();
        }
    }
}
