using Company.R.BLL.Interfaces;
using Company.R.DAL.DBContexts;
using Company.R.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.R.BLL.Repositorys
{
    public class GenericReposiotry<T> : IGenericReposiotry<T> where T : BaseEntity
    {
        private protected CompanyDBContext _dbcontext;
        public GenericReposiotry(CompanyDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public int Add(T entity)
        {
              _dbcontext.Add(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbcontext.Remove(entity);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
         return   _dbcontext.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            //return _dbcontext.Set<T>().FirstOrDefault(x=>x.Id==id);
            return _dbcontext.Set<T>().Find(id);


        }

        public int Update(T entity)
        {
            _dbcontext.Update(entity);
            return _dbcontext.SaveChanges();
        }
    }
}
