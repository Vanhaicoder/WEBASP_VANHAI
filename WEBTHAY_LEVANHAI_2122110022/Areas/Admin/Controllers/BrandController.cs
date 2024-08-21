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
    public class BrandController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        // GET: Admin/Brand
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

            // Query brands based on the search filter
            var listBrand = db.Brands.AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                listBrand = listBrand.Where(n => n.Name.Contains(SearchString));
            }

            // Order by descending Id
            listBrand = listBrand.OrderByDescending(n => n.Id);

            // Pagination settings
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(listBrand.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand objBrand)
        {
            if (ModelState.IsValid)
            {
                if (objBrand.ImageUpload != null && objBrand.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objBrand.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/brands/"), fileName);
                    objBrand.ImageUpload.SaveAs(path);
                }

                db.Brands.Add(objBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBrand = db.Brands.Find(id);
            if (objBrand == null)
            {
                return HttpNotFound();
            }

            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBrand = db.Brands.Find(id);
            if (objBrand == null)
            {
                return HttpNotFound();
            }

            return View(objBrand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var brandToDelete = db.Brands.Find(id);
            if (brandToDelete != null)
            {
                db.Brands.Remove(brandToDelete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = db.Brands.Find(id);
            if (objBrand == null)
            {
                return HttpNotFound();
            }

            return View(objBrand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand objBrand)
        {
            if (ModelState.IsValid)
            {
                if (objBrand.ImageUpload != null && objBrand.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objBrand.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/brands/"), fileName);
                    objBrand.ImageUpload.SaveAs(path);
                }

                db.Entry(objBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objBrand);
        }
    }
}
