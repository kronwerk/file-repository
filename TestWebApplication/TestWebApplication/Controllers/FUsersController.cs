using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class FUsersController : Controller
    {
        private UserDBContext db = new UserDBContext();

        // GET: FUsers
        public ActionResult Index()
        {
            return View(db.FUsers.ToList());
        }

        // GET: FUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FUser fUser = db.FUsers.Find(id);
            if (fUser == null)
            {
                return HttpNotFound();
            }
            return View(fUser);
        }

        // GET: FUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FUserID,FirstName,LastName,age,gender,city")] FUser fUser)
        {
            if (ModelState.IsValid)
            {
                db.FUsers.Add(fUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fUser);
        }

        // GET: FUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FUser fUser = db.FUsers.Find(id);
            if (fUser == null)
            {
                return HttpNotFound();
            }
            return View(fUser);
        }

        // POST: FUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FUserID,FirstName,LastName,age,gender,city")] FUser fUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fUser);
        }

        // GET: FUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FUser fUser = db.FUsers.Find(id);
            if (fUser == null)
            {
                return HttpNotFound();
            }
            return View(fUser);
        }

        // POST: FUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FUser fUser = db.FUsers.Find(id);
            db.FUsers.Remove(fUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
