using System;
using System.Collections.Generic;
using System.Text;
using University.NetStandart.Core.Models;

namespace University.NetStandart.Core.IRepositories
{
    public interface IStudentListRepository :IRepository<StudentList>
    {
        IEnumerable<StudentList> GetStudentLists();
        IEnumerable<StudentList> GetFirstSix();
        IEnumerable<StudentList> GetLastSix();
    }
}
