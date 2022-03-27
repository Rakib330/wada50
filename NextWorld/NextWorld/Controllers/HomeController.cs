using NextWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextWorld.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        TraineeDbContext db = new TraineeDbContext();
        public ActionResult Index(int pg=1)
        {
            ViewBag.CurrentPage = pg;
            int perPage = 5;
            ViewBag.TotalPages = (int)Math.Ceiling((double)db.Trainees.Count() / perPage);
            var data = db.Trainees.OrderBy(x => x.TraineeNo)
                                  .Skip((pg - 1) * perPage)
                                  .Take(perPage)
                                  .ToList();
            
            return View(data);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Trainee p)
        {
            if (ModelState.IsValid)
            {
                db.Trainees.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public ActionResult Edit(int id)
        {
            return View(db.Trainees.First(x=>x.TraineeNo == id));
        }
        [HttpPost]
        public ActionResult Edit(Trainee p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Trainees.First(x => x.TraineeNo == id));
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteDo(int id)
        {
            db.Entry(new Trainee{TraineeNo=id}).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}