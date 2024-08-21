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
    public class BannerController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();

        // GET: Admin/Banner
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            // Handle the search query
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            // Query banners based on the search filter
            var listBanner = db.Banners.AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                listBanner = listBanner.Where(n => n.Name.Contains(SearchString));
            }

            // Order by descending Id
            listBanner = listBanner.OrderByDescending(n => n.Id);

            // Pagination settings
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(listBanner.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Banner objBanner)
        {
            if (ModelState.IsValid)
            {
                if (objBanner.ImageUpload != null && objBanner.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBanner.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBanner.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objBanner.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/banners/"), fileName);
                    objBanner.ImageUpload.SaveAs(path);
                }

                db.Banners.Add(objBanner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objBanner);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBanner = db.Banners.Find(id);
            if (objBanner == null)
            {
                return HttpNotFound();
            }

            return View(objBanner);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBanner = db.Banners.Find(id);
            if (objBanner == null)
            {
                return HttpNotFound();
            }

            return View(objBanner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bannerToDelete = db.Banners.Find(id);
            if (bannerToDelete != null)
            {
                db.Banners.Remove(bannerToDelete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBanner = db.Banners.Find(id);
            if (objBanner == null)
            {
                return HttpNotFound();
            }

            return View(objBanner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Banner objBanner)
        {
            if (ModelState.IsValid)
            {
                if (objBanner.ImageUpload != null && objBanner.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBanner.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBanner.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objBanner.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/banners/"), fileName);
                    objBanner.ImageUpload.SaveAs(path);
                }

                db.Entry(objBanner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objBanner);
        }
    }
}
