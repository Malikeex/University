using System;
using System.Collections.Generic;
using System.Text;
using University.NetStandart.Core.Models;

namespace University.NetStandart.Core.IRepositories
{
    public interface IStudentsRepository:IRepository<Students>
    {
        int GetCountMan();
        int GetCountWoman();
       
        IEnumerable<Students> GetReport();
        int GetEndWith();
        bool IsExistWoman();
       

    }
}
