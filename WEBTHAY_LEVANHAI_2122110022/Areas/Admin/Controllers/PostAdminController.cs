using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly WebBanHangEntities db = new WebBanHangEntities();

        // GET: Admin/Post
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var posts = db.Posts.Include(p => p.Topic).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(p => p.Title.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 4;
            int pageNumber = page ?? 1;

            posts = posts.OrderByDescending(p => p.Id);

            return View(posts.ToPagedList(pageNumber, pageSize)); // This should be IPagedList<Post>
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                if (post.ImageUpload != null && post.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(post.ImageUpload.FileName);
                    string extension = Path.GetExtension(post.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    post.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/posts/"), fileName);
                    post.ImageUpload.SaveAs(path);
                }

                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "Name", post.Topic_id);
            return View(post);
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var post = db.Posts.Include(p => p.Topic).FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var post = db.Posts.Find(id);
            if (post != null)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "Name", post.Topic_id);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                if (post.ImageUpload != null && post.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(post.ImageUpload.FileName);
                    string extension = Path.GetExtension(post.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    post.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/posts/"), fileName);
                    post.ImageUpload.SaveAs(path);
                }

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Topic_id = new SelectList(db.Topics, "Topic_id", "Name", post.Topic_id);
            return View(post);
        }
    }
}
