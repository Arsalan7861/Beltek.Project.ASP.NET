using Beltek.Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Beltek.Project.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new SchoolDbContext())
            {
                var lst = db.Classes.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddClass(Class c)
        {
            using (var db = new SchoolDbContext())
            {
                db.Classes.Add(c);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditClass(int id)
        {
            using (var db = new SchoolDbContext())
            {               
                var cls = db.Classes.Find(id);
                return View(cls);
            }
        }

        [HttpPost]
        public IActionResult EditClass(Class cls)
        {
            using (var db = new SchoolDbContext())
            {
                var existingClass = db.Classes.FirstOrDefault(c => c.Classid == cls.Classid);

                if (existingClass != null) {
                    existingClass.ClassName = cls.ClassName;
                    existingClass.Quota = cls.Quota;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
                return BadRequest("Class not Found!");
            }
        }

        public IActionResult DeleteClass(int id)
        {
            using (var db = new SchoolDbContext())
            {                
                db.Classes.Remove(db.Classes.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
