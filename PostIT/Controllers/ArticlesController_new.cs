using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PostIT.DAL;
using PostIT.Models;
using System.Threading;
using System.Globalization;
using System.Data.Entity.Infrastructure;

namespace PostIT.Controllers
{
    public class ArticlesController_new : BaseController
    {
        private ArcticleContext db = new ArcticleContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            TagsDropDownList();
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,ShortDescription,Created,Modified,Published,Category")] Article article )
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    article.Created = DateTime.Now;
                    db.Articles.Add(article);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            TagsDropDownList(article.CategoryID);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            TagsDropDownList(article.CategoryID);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var articleToUpdate = db.Articles.Find(id);
            if (TryUpdateModel(articleToUpdate, "",
               new string[] { "Title", "Content", "ShortDescription", "Created", "Modified", "Published", "CategoryID" }))
            {
                try
                {
                    Article article = db.Articles.Find(id);
                    article.Modified = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            TagsDropDownList(articleToUpdate.CategoryID);
            return View(articleToUpdate);
        }

        private void TagsDropDownList(object selectedTag = null)
        {
            var TagsQuery = from d in db.Tags
                                   orderby d.Name
                                   select d;
            ViewBag.CategoryID = new SelectList(TagsQuery, "CategoryID", "Name", selectedTag);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id )
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
