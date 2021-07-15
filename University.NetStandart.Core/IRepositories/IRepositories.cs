using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using University.NetStandart.Core.Models;

namespace University.NetStandart.Core.IRepositories
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        Task<T> InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Remove(T entity);
        void SaveChanges();
       
    }
}
