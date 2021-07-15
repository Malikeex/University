using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL;
using University.NetStandart.DAL.DbContext;

namespace University.Web.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    //[Route("[controller]/[action]")]
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentsController : Controller
    {

        private readonly ApplicationDbContext students_context;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        public StudentsController(ApplicationDbContext _students_context, IUnitOfWorkFactory _unitOfWorkFactory)
        {

            students_context = _students_context;
            unitOfWorkFactory = _unitOfWorkFactory;


        }
        [HttpGet]
        public ActionResult GetStudentsList()
        {
            IEnumerable<Students> students;
            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
                students = unitOfWork.Students.GetAll();

            }
            return Ok(students);
        }


        // GET: StdentsController/Create


        [HttpPost]
        public ActionResult CreateStudent(Students student)
        {

            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
                var result = unitOfWork.Students.InsertAsync(student);

            }

            return Ok();
        }



        // GET: StdentsController/Edit/5
        [HttpPost]
        public ActionResult UpdateStudent(Students student)
        {

            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
                unitOfWork.Students.Update(student);

            }
            return Ok(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(int id)
        {

            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
                unitOfWork.Students.Delete(id);

            }
           return  Ok();

        }
    }
}
