using Company.R.BLL.Interfaces;
using Company.R.BLL.Repositorys;
using Company.R.DAL.DBContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.BLL
{
    public class UnitOfWork : IUnitOfWork
    {

        private CompanyDBContext dbcontex;
        private IEmployeeRepository employeeRepository;
        private IDepartmentRepository departmentRepository;

        public UnitOfWork(CompanyDBContext _dbcontex)
        {
            dbcontex= _dbcontex;
            employeeRepository=new EmployeeRepository(_dbcontex);
            departmentRepository = new DepartmentRepository(_dbcontex);

        }
        public IEmployeeRepository EmployeeRepository => employeeRepository;

        public IDepartmentRepository DepartmentRepository => departmentRepository;
    }
}
