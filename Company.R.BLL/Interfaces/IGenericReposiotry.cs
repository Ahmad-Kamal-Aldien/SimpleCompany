using Company.R.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.BLL.Interfaces
{
    public interface IGenericReposiotry<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        int Add(T entity);
        int Delete(T entity);
        int Update(T entity);
    }
}
