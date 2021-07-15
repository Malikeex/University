using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using University.NetStandart.Core.IRepositories;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL.DbContext;

namespace University.NetStandart.DAL.Repositories
{
    public class StudentsRepository:Repository<Students>, IStudentsRepository
    {
        public StudentsRepository(ApplicationDbContext context):base(context)
        {
            DbSet = context.Students;
        }

        public bool IsExistWoman()
        {
            return Queryable.Any(DbSet, x => x.Sex.Equals("жен"));
        }
       

        public int GetCountMan()
        {
            return Queryable.Where(DbSet, x => x.Sex.Equals("муж")).Count();

        }
        public int GetCountWoman()
        {
            return Queryable.Where(DbSet, x => x.Sex.Equals("жен")).Count();

        }

        public int GetEndWith()
        {
            return Queryable.Where(DbSet, x => x.Name.EndsWith("ов")).Count();
        }

        public IEnumerable<Students> GetReport()
        {
            return Queryable.Where(DbSet, x => x.Name.Contains("и")).ToList();
            
           // return Queryable.Join(DbSet, x => x.Name, y.Name,(x,y) => { Name = x.Name})
            // return Queryable.Where(DbSet, x => x.Name.Take(2)).ToList();
            // return
        }

        public IEnumerable<Students> InsertAsync()
        {
            throw new NotImplementedException();
        }

       

        //public override void Delete(int id)
        //{
        //    var student = Queryable.Where(DbSet, x => x.Id == id).FirstOrDefault();
        //    DbSet.Remove(student);
        //}
    }
}
