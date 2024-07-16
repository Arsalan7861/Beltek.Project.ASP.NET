using Beltek.Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Beltek.Project.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new SchoolDbContext())
            {
                var lstc = db.Classes.ToList();
                var lst = db.Students.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            using (var db = new SchoolDbContext())
            {
                // Check if there are any classes in the Class table
                if (!db.Classes.Any())
                {
                    ViewBag.ErrorMessage = "No classes available. Please add a class before adding a student.";
                    return View();                    
                }

                var lst = db.Students.ToList();
                ViewBag.Classes = db.Classes.Select(s => new SelectListItem
                {
                    Value = s.Classid.ToString(),
                    Text = s.ClassName
                }).ToList();
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddStudent(Student std)
        {
            using (var db = new SchoolDbContext())
            {
                db.Students.Add(std);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int id)
        {
            using (var db = new SchoolDbContext())
            {
                var lst = db.Classes.ToList();
                ViewBag.Classes = db.Classes.Select(s => new SelectListItem
                {
                    Value = s.Classid.ToString(),
                    Text = s.ClassName
                }).ToList();
                
                var std = db.Students.Find(id);

                return View(std);
            }
        }

        [HttpPost]
        public IActionResult EditStudent(Student std)
        {
            using (var db = new SchoolDbContext())
            {

                var existingStudent = db.Students.Include(s => s.Class).FirstOrDefault(s => s.Studentid == std.Studentid);
                if (existingStudent != null)
                {
                    existingStudent.Name = std.Name;
                    existingStudent.Surname = std.Surname;
                    existingStudent.Number = std.Number;
                    existingStudent.Class.ClassName = std.Class.ClassName;
                    existingStudent.Class.Quota = std.Class.Quota;

                    if (existingStudent.Classid != std.Class.Classid)
                    {
                        var newClass = db.Classes.Find(std.Class.Classid);
                        if (newClass != null)
                        {
                            existingStudent.Classid = newClass.Classid;
                            existingStudent.Class = newClass;
                        }
                        else
                        {
                            return BadRequest("Invalid Class ID.");
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return BadRequest("Student not found!");
            }
        }

        public IActionResult DeleteStudent(int id)
        {
            using (var db = new SchoolDbContext())
            {
                var std = db.Students.Find(id);
                db.Students.Remove(std);
                db.Classes.Remove(db.Classes.Find(std.Classid));
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}