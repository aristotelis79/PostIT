using PostIT.DAL;
using PostIT.Models;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PostIT.Controllers
{
    public class ArticlesController : BaseController
    {
        private ArcticleContext db = new ArcticleContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create([Bind(Include = "ID,Title,Content,ShortDescription,Created,Modified,Published,CategoryID")] Article article, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                article = SaveImageIfExist(article, upload);
                article.Created = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TagsDropDownList(article.CategoryID);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = db.Articles.Find(id);
            if (TryUpdateModel(article, "", new string[] { "Title", "Content", "ShortDescription", "Created", "Modified", "Published", "CategoryID", "FilePathID" }))
            {
                try
                {
                    article.Modified = DateTime.Now;
                    article = SaveImageIfExist(article, upload);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes");
                }
            }
            TagsDropDownList(article.CategoryID);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            FilePath image = new FilePath();
            if (article.FilePathID != null)
            {
                image = db.FilePaths.Find(article.FilePathID);
                db.FilePaths.Remove(image);
            }
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void TagsDropDownList(object selectedCategory = null)
        {
            var CategoryQuery = from d in db.Tags
                                orderby d.Name
                                select d;
            ViewBag.CategoryID = new SelectList(CategoryQuery, "ID", "Name", selectedCategory);
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