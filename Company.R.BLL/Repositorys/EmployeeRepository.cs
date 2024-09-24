using Company.R.BLL.Interfaces;
using Company.R.BLL.Repositorys;
using Company.R.DAL.DBContexts;
using Company.R.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.BLL.Repositorys
{
    public class EmployeeRepository:GenericReposiotry<Employee>,IEmployeeRepository
    {
    
        public EmployeeRepository(CompanyDBContext _dbcontext):base(_dbcontext)
        {
            
        }

        public IEnumerable<Employee> getDataSearch(string name)
        {
            //IEnumerable<Employee> getEmps= _dbcontext.Employee.Where(x=>x.Name.ToLower().Contains(name.ToLower())).Include(x=>x.Dept_id).ToList();
            IEnumerable<Employee> getEmps = _dbcontext.Employee.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            return getEmps;
        }
    }
}
