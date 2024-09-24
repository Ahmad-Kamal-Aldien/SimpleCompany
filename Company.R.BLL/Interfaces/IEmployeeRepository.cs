using Company.R.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericReposiotry<Employee>
    {
        IEnumerable<Employee> getDataSearch(string name);
    }
}
