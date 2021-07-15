using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using University.NetStandart.Core.IRepositories;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL.DbContext;

namespace University.NetStandart.DAL.Repositories
{
    public class LessonsRepository:Repository<Lessons>,ILessonsRepository
    {
        public LessonsRepository(ApplicationDbContext context) : base(context)
        {
            DbSet = context.Lessons;
        }

        public double GetAverage(int studentId)
        {
            return Queryable.Where(DbSet, x => x.StudentId == studentId).Average(x=> x.Point);

        }

        public IEnumerable<Lessons> GetListByStudentId(int studentId)
        {
            return  Queryable.Where(DbSet, x => x.StudentId == studentId).ToList();
        }

        public double GetMax(int studentId)
        {
            return Queryable.Where(DbSet, x => x.StudentId == studentId).Max(x => x.Point);
        }

       //// public IEnumerable<Report1> GetReport()
       // {
       //     // return Queryable.GroupBy(DbSet, x => x.Name).Select(y => new Report1 { LessonsName = y.Key, CountOfStudents = y.Count() }).ToList();
       // }
    }
}
