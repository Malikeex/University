using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.NetStandart.Core.IRepositories;
using University.NetStandart.Core.Models;

namespace University.NetStandart.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentsRepository Students { get; }
        ILessonsRepository Lessons { get; }
        IStudentListRepository StudentList { get; }

        Task<int> CompleteAsync();
        void BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    }
}
