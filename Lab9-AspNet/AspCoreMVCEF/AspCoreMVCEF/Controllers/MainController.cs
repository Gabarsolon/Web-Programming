using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AspCoreMVCEF.Models;
using AspCoreMVCEF.Data;
using AspCoreMVCEF.DataAbstractionLayer;

namespace AspCoreMVCEF.Controllers
{
    public class MainController : Controller
    {
        private readonly DBWpContext _context;

        public MainController(DBWpContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View("FilterStudents");
        }

        public string Test()
        {
            return "It's working";
        }
        public string Test1(string param1 = "hello", int param2 = 0)
        {
            return "Result: " + param1 + param2.ToString();
        }

        public IActionResult testGetStudent()
        {
            Student stud = new Student();

            stud.Id = 10;
            stud.Name = "Nume1";
            stud.Password = "Pass1";
            stud.Group_id = 1;

            ViewData["student"] = stud;
            return View("ViewStudent");
        }

        public ActionResult FilterStudents()
        {
            return View("FilterStudents");
        }

        public IActionResult GetStudents()
        {
            List<Student> slist = _context.Student.ToList();

            /*DAL dal = new DAL();
            List<Student> slist = dal.GetAllStudents();*/
            ViewData["studentList"] = slist;
            return View("ViewGetStudents");
        }

        public IActionResult AddStudent()
        {
            return View("AddNewStudent");
        }

        public IActionResult SaveStudent(int id, string name, string password, int group_id)
        {
            Student stud = new Student();
            stud.Id = id;
            stud.Name = name;
            stud.Password = password;
            stud.Group_id = group_id;

            /*DAL dal = new DAL();
            dal.SaveStudent(stud);*/
            _context.Add(stud);
            _context.SaveChanges();
            return RedirectToAction("GetStudents");
        }


        public string GetStudentsFromGroup(int group_id)
        {
            /*DAL dal = new DAL();
            List<Student> slist = dal.GetStudentsFromGroup(group_id);*/
            List<Student> slist = _context.Student.Where(stud => stud.Group_id == group_id).ToList();

            string result = "<table><thead><th>Id</th><th>Nume</th><th>Password</th><th>Group_Id</th></thead>";

            foreach (Student stud in slist)
            {
                result += "<tr><td>" + stud.Id + "</td><td>" + stud.Name + "</td><td>" + stud.Password + "</td><td>" + stud.Group_id + "</td><td></tr>";
            }

            result += "</table>";
            return result;
        }

    }
}
