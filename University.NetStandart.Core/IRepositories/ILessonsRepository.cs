using System;
using System.Collections.Generic;
using System.Text;
using University.NetStandart.Core.Models;

namespace University.NetStandart.Core.IRepositories
{
    public interface ILessonsRepository : IRepository<Lessons>
    {
        IEnumerable<Lessons> GetListByStudentId(int studentId);
        double GetAverage(int studentId);
        double GetMax(int studentId);
       
    }
     
}
