using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL;
using University.NetStandart.DAL.DbContext;

namespace University.Web.Controllers
{
    //[Route("[controller]")]
    // [ApiController]
    //[Route("[controller]/[action]")]
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize]
    public class StudentsController : Controller
    {

        private readonly ApplicationDbContext students_context;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        public StudentsController(ApplicationDbContext _students_context,IUnitOfWorkFactory _unitOfWorkFactory)
        {
            
            students_context = _students_context;
            unitOfWorkFactory = _unitOfWorkFactory;


        }
        
        public ActionResult Index()
        {
            IEnumerable<Students> students;
            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
               students = unitOfWork.Students.GetAll();
               
            }
            return View(students);
        }


        // GET: StdentsController/Create

        //[HttpGet]
        //[Route("[action]")]
        //[Route("api/Employee/GetEmployees")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Students student)
        {
            

            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
               var result = unitOfWork.Students.InsertAsync(student);
               
            }

            return RedirectToAction("Index");
        }

       

        // GET: StdentsController/Edit/5
        public ActionResult Update(int id)
        {
            Students student = new Students();
            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
               student=unitOfWork.Students.Get(id);

            }
            return View(student);
        }

        // POST: StdentsController/Edit/5

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Update(Students student)
        {
            
            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork()) 
            {
              unitOfWork.Students.Update(student);
                         
            }
                return RedirectToAction("Index");
        }

        // GET: StdentsController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: StdentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Students students)
        {
            
            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
                unitOfWork.Students.Delete(students);

            }
            return RedirectToAction("Index");
            
        }
    }
}
