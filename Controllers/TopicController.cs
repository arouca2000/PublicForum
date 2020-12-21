using Microsoft.AspNet.Identity;
using PublicForum.Models;
using PublicForum.Models.DTO;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PublicForum.Controllers
{
    public class TopicController : Controller
    {
        private PublicForumDbContext db = new PublicForumDbContext();

        // GET: Topic
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var user = db.AspNetUsers.Single(a => a.Id == userId);
                var topicsDTO = (from tp in db.Topics
                                 select new TopicDTO
                                 {
                                     Id = tp.Id,
                                     Title = tp.Title,
                                     Description = tp.Description,
                                     CreateData = tp.CreateData,
                                     TopicCreator = tp.User.Id == user.Id
                                 });

                return View(topicsDTO);
            }
        }

        // GET: Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CreateData")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                topic.CreateData = DateTime.Now;
                topic.User = db.AspNetUsers.Single(a => a.Id == userId);
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreateData")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                if (topic.User == null)
                {
                    var userId = User.Identity.GetUserId();
                    topic.User = db.AspNetUsers.Single(a => a.Id == userId);
                }
                if (topic.CreateData == DateTime.MinValue)
                {
                    topic.CreateData = DateTime.Now;
                }
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
