using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using Test3Class.Models;
using Test3Class.ViewModel;

namespace Test3Class.Controllers
{
    public class HomeController : Controller
    {
        StudentDbContext db;
        IWebHostEnvironment env;
        public HomeController(StudentDbContext db, IWebHostEnvironment env) { this.db= db; this.env = env; }
        public IActionResult Index()
        {
            return View(db.Students.Include(x => x.Subject).ToList());
        }
        public IActionResult _Table()
        {
            return PartialView();
        }
        public IActionResult Add()
        {
            ViewBag.Subs = db.Subjects.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(StudentModel d)
        {
            if (ModelState.IsValid)
            {
                var data = new Student
                {
                    StudentName = d.StudentName,
                    Roll = d.Roll,
                    Status = d.Status,
                    SubjectId = d.SubjectId,
                    StudentId = d.StudentId
                };
                var ext = Path.GetExtension(d.Photo.FileName);
                var f = Guid.NewGuid()+ext;
                d.Photo.CopyTo(new FileStream(env.WebRootPath+"\\Uploads\\"+f,FileMode.Create));
                data.Photo = f;
                db.Students.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subs = db.Subjects.ToList();
            return View(d);
        }
        public IActionResult Edit(int id)
        {
            var data = db.Students.First(x => x.StudentId == id);
            ViewBag.Subs = db.Subjects.ToList();
            ViewBag.Photo = data.Photo;
            return View(new StudentModel
            {
                StudentName = data.StudentName,
                StudentId=data.StudentId,
                Roll = data.Roll,
                SubjectId = data.SubjectId,
                Status = data.Status, 
            });
        }
        [HttpPost]
        public IActionResult Edit(StudentModel d)
        {
            var sata = db.Students.First(x => x.StudentId == d.StudentId);
            ViewBag.Subs = db.Subjects.ToList();
            ViewBag.Photo = sata.Photo;
            if (ModelState.IsValid)
            {

                sata.StudentName = d.StudentName;
                sata.Roll = d.Roll;
                sata.Status = d.Status;
                sata.SubjectId = d.SubjectId;
                sata.StudentId = d.StudentId;
                if (d.Photo != null)
                {
                    var ext = Path.GetExtension(d.Photo.FileName);
                    var f = Guid.NewGuid() + ext;
                    d.Photo.CopyTo(new FileStream(env.WebRootPath + "\\Uploads\\" + f, FileMode.Create));
                    sata.Photo = f;
                }
                db.Entry(sata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subs = db.Subjects.ToList();
            return View(d);
        }
        public IActionResult Delete(int id)
        {
            db.Entry(new Student { StudentId = id}).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
