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
    public class CategoryAdminController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();

        // GET: Admin/CategoryAdmin
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            // Cập nhật số trang khi tìm kiếm
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            // Lấy danh sách danh mục theo từ khóa tìm kiếm hoặc tất cả danh mục nếu không tìm kiếm
            var listCategory = db.Categories
                .Where(n => string.IsNullOrEmpty(SearchString) || n.name.Contains(SearchString))
                .OrderByDescending(n => n.id)
                .ToList();

            ViewBag.CurrentFilter = SearchString;

            // Số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(listCategory.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Cung cấp dữ liệu cho dropdown list với lựa chọn "None" để chọn danh mục cha
            var categories = db.Categories.ToList();
            categories.Insert(0, new Category { id = 0, name = "None" }); // Thêm lựa chọn "None"

            ViewBag.Parent_id = new SelectList(categories, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category objCategory)
        {
            try
            {
                if (objCategory.ImageUpload != null && objCategory.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    objCategory.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/categories/"), fileName);
                    objCategory.ImageUpload.SaveAs(path);
                }

                // Nếu chọn "None", đặt Parent_id = null
                if (objCategory.Parent_id == 0)
                {
                    objCategory.Parent_id = null;
                }

                db.Categories.Add(objCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                // Cung cấp dữ liệu cho dropdown list để người dùng chọn lại
                var categories = db.Categories.ToList();
                categories.Insert(0, new Category { id = 0, name = "None" }); // Thêm lựa chọn "None"
                ViewBag.Parent_id = new SelectList(categories, "id", "name");
                return View(objCategory);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = db.Categories.Find(id);
            if (objCategory == null)
            {
                return HttpNotFound();
            }

            // Cung cấp dữ liệu cho dropdown list với lựa chọn "None"
            var categories = db.Categories.ToList();
            categories.Insert(0, new Category { id = 0, name = "None" }); // Thêm lựa chọn "None"

            ViewBag.Parent_id = new SelectList(categories, "id", "name", objCategory.Parent_id);
            return View(objCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category objCategory)
        {
            try
            {
                if (objCategory.ImageUpload != null && objCategory.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    objCategory.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/categories/"), fileName);
                    objCategory.ImageUpload.SaveAs(path);
                }

                // Nếu chọn "None", đặt Parent_id = null
                if (objCategory.Parent_id == 0)
                {
                    objCategory.Parent_id = null;
                }

                db.Entry(objCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                // Cung cấp dữ liệu cho dropdown list để người dùng chọn lại
                var categories = db.Categories.ToList();
                categories.Insert(0, new Category { id = 0, name = "None" }); // Thêm lựa chọn "None"
                ViewBag.Parent_id = new SelectList(categories, "id", "name", objCategory.Parent_id);
                return View(objCategory);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = db.Categories.Find(id);
            if (objCategory == null)
            {
                return HttpNotFound();
            }
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = db.Categories.Find(id);
            if (objCategory == null)
            {
                return HttpNotFound();
            }
            return View(objCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CFDelete(Category objCater)
        {
            // Tìm danh mục dựa trên id
            var objCategory = db.Categories.Where(n => n.id == objCater.id).FirstOrDefault();

            if (objCategory != null)
            {
                // Xóa danh mục khỏi cơ sở dữ liệu
                db.Categories.Remove(objCategory);
                db.SaveChanges();
            }

            // Điều hướng về trang Index sau khi xóa
            return RedirectToAction("Index");
        }

    }
}
