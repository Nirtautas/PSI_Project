using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Data;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CourseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> objCourseList = _db.Courses;
            return View(objCourseList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course objCourse)
        {
            if (ModelState.IsValid) {
                _db.Courses.Add(objCourse);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objCourse);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var courseFromDatabase = _db.Courses.Find(id);
            if (courseFromDatabase == null)
                return NotFound();

            return View(courseFromDatabase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course objCourse)
        {
            if (ModelState.IsValid)
            {
                _db.Courses.Update(objCourse);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objCourse);
        }
    }
}
