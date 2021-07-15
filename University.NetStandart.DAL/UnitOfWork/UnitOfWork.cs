
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.NetStandart.Core.IRepositories;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL.DbContext;
using University.NetStandart.DAL.Repositories;

namespace University.NetStandart.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        public IStudentsRepository Students { get; }
        public ILessonsRepository Lessons { get; }
        public IStudentListRepository StudentList { get; }


        private IDbContextTransaction _transaction;



        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<Type, object>();
            Students = new StudentsRepository(context);
            Lessons = new LessonsRepository(context);
            StudentList = new StudentListRepository(context);
            
          
        }


        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }


        public void CommitTransaction()
        {
            if (_transaction == null) return;

            _transaction.Commit();
            _transaction.Dispose();

            _transaction = null;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            return _repositories.GetOrAdd(typeof(TEntity), (object)new Repository<TEntity>(_context)) as IRepository<TEntity>;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();

            _transaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context.Dispose();

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
