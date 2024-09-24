using Company.R.BLL.Interfaces;
using Company.R.DAL.DBContexts;
using Company.R.DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.BLL.Repositorys
{
    public class DepartmentRepository : GenericReposiotry<Department>,IDepartmentRepository
    {
        public DepartmentRepository(CompanyDBContext _dbcontext) : base(_dbcontext)
        {
            
        }
    }
}
