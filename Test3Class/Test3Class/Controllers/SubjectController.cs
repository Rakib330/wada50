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
    public class SubjectController : Controller
    {
        StudentDbContext db;
        public SubjectController(StudentDbContext db) { this.db= db; }
        public IActionResult Index()
        {
            return View(db.Subjects.Include(x=>x.Student).ToList());
        }
        public IActionResult _Table()
        {
            return PartialView();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Subject d)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(d);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subs = db.Subjects.ToList();
            return View(d);
        }
        public IActionResult Edit(int id)
        {
            var data = db.Subjects.First(x => x.SubjectId == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Subject d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(d);
        }
        public IActionResult Delete(int id)
        {
            db.Entry(new Subject { SubjectId = id}).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
