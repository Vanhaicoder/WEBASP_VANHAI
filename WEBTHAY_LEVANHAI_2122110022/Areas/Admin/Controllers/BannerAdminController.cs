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
        WebBanHangEntities db = new WebBanHangEntities();

        // GET: Admin/Banner
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var listBanner = new List<Banner>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Lấy danh sách banner theo từ khóa tìm kiếm
                listBanner = db.Banners.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                // Lấy tất cả banner trong bảng Banner
                listBanner = db.Banners.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            // Số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            // Sắp xếp theo id banner, banner mới đưa lên đầu
            listBanner = listBanner.OrderByDescending(n => n.Id).ToList();
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
            try
            {
                // Kiểm tra nếu có hình ảnh được tải lên
                if (objBanner.ImageUpload != null && objBanner.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBanner.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBanner.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objBanner.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/banners/"), fileName);
                    objBanner.ImageUpload.SaveAs(path);
                }

                // Thêm banner vào cơ sở dữ liệu
                db.Banners.Add(objBanner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (có thể thêm ghi log hoặc thông báo lỗi cho người dùng)
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                return View(objBanner);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objBanner = db.Banners.Where(n => n.Id == id).FirstOrDefault();
            return View(objBanner);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objBanner = db.Banners.Where(n => n.Id == id).FirstOrDefault();
            return View(objBanner);
        }

        [HttpPost]
        public ActionResult Delete(Banner objBanner)
        {
            var bannerToDelete = db.Banners.Where(n => n.Id == objBanner.Id).FirstOrDefault();
            db.Banners.Remove(bannerToDelete);
            db.SaveChanges();
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
        public ActionResult Edit(Banner objBanner)
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
    }
}
