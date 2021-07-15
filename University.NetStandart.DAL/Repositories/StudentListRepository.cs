using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using University.NetStandart.Core.IRepositories;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL.DbContext;

namespace University.NetStandart.DAL.Repositories
{

    public class StudentListRepository : Repository<StudentList>, IStudentListRepository
    {
        public StudentListRepository(ApplicationDbContext context) : base(context)
        {
            DbSet = context.StudentList;
        }

        public IEnumerable<StudentList> GetFirstSix()
        {
            return Enumerable.Take(DbSet, 6);
        }
        public IEnumerable<StudentList> GetLastSix()
        {
            return Enumerable.Skip(DbSet, 6).Take(6).ToList();
        }

        public IEnumerable<StudentList> GetStudentLists()
        {
            throw new NotImplementedException();
        }

    }
}
