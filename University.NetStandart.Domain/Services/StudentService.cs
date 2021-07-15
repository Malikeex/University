using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL;

namespace University.NetStandart.Domain.Services
{
    public interface IStudentService 
    {
        Task<EntityOperationResult<Students>> CreateStudentAsync(Students student);
    }
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public StudentService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<EntityOperationResult<Students>> CreateStudentAsync(Students student)
        {
            using (var unitOfWork = _unitOfWorkFactory.MakeUnitOfWork())
            {
                try
                { 
                    var entity = await unitOfWork.Students.InsertAsync(student);
                    await unitOfWork.CompleteAsync();

                    return EntityOperationResult<Students>.Success(entity);
                }
                catch (Exception ex)
                {
                    return EntityOperationResult<Students>.Failure().AddError(ex.Message);
                }
            }
        }
        

    }
}
