using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.NetStandart.Core.IRepositories;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL.DbContext;

namespace University.NetStandart.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> DbSet { get; set; }


        public Repository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }


        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }



        public void Update(T entity)
        {
            DbSet.Update(entity);
            _context.SaveChanges();
           
        }

        public virtual void Delete (int id)
        {
            if (id >null)
            {
                throw new ArgumentNullException("id");
            }
            var entity = DbSet.Where(x => x.Id == id).FirstOrDefault();
            DbSet.Remove(entity);
            _context.SaveChanges();
        }

      
        public  void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbSet.Remove(entity);
            _context.SaveChanges();
          
        }

        public virtual T Get(int Id)
        {
            return DbSet.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbSet.Add(entity);
            _context.SaveChanges();
        }

       

        public async Task<T> InsertAsync(T entity)
        {
            var entry = await DbSet.AddAsync(entity);

            _context.SaveChanges();
           
            return entry.Entity;
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
