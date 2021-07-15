using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL;
using University.NetStandart.DAL.DbContext;
using PagedList;
using System;
using System.Linq;
using PagedList.Core;

using Microsoft.AspNetCore.Http;

namespace University.Web.Main.Controllers
{
    // [Route("[controller]")]
    // [ApiController]
    //[Route("[controller]/[action]")]
    //[Route("api/[controller]")]
    //[ApiController]
    //[Authorize]
    public class StudentsController : Controller
    {

        private readonly ApplicationDbContext students_context;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        public StudentsController(ApplicationDbContext _students_context, IUnitOfWorkFactory _unitOfWorkFactory)
        {

            students_context = _students_context;
            unitOfWorkFactory = _unitOfWorkFactory;


        }

        public ViewResult Index(/*string sortOrder, string currentFilter, string searchString, int? page*/)
        {
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }
            IEnumerable<Students> students;
            using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
            {
                students = unitOfWork.Students.GetAll();

            }
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        students = students.OrderByDescending(s => s.Name);
            //        break;
            //    default:  // Name ascending 
            //        students = students.OrderBy(s => s.Name);
            //        break;
            //}
            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));

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
                student = unitOfWork.Students.Get(id);

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
        [HttpPost]
        
        public ActionResult DeleteStudent([FromBody] int studentId)
        {
           // var id = 1;
            if (studentId != null)
            {
                using (var unitOfWork = unitOfWorkFactory.MakeUnitOfWork())
                {
                    unitOfWork.Students.Delete(studentId);
                    return RedirectToAction("Index");

                }
               
            }
            else  return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        }
    }
}
