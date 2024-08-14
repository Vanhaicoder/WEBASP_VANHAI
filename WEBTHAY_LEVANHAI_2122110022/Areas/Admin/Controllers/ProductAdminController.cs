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
    public class ProductAdminController : Controller
    {
        WebBanHangEntities db = new WebBanHangEntities();

        // GET: Admin/ProductAdmin
        public ActionResult Index(string SearchString,string currentFilter, int? page)
        {
           var listProduct = new List<Product>();
            if(SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if(!string.IsNullOrEmpty(SearchString))
            {
                //lấy ds sản phẩm theo từ khóa tìm kiếm
                listProduct = db.Products.Where(n =>n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy tất cả sản phẩm trong bảng Product
                listProduct = db.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Lấy danh sách các danh mục từ database
            var categories = db.Categories.ToList();
            ViewBag.Category_id = new SelectList(categories, "Id", "Name");

            // Lấy danh sách các thương hiệu từ database
            var brands = db.Brands.ToList();
            ViewBag.Brand_id = new SelectList(brands, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product objProduct)
        {
            try
            {
                // Kiểm tra nếu có hình ảnh được tải lên
                if (objProduct.ImageUpload != null && objProduct.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                    string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objProduct.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                    objProduct.ImageUpload.SaveAs(path);
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                db.Products.Add(objProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (có thể thêm ghi log hoặc thông báo lỗi cho người dùng)
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                // Lấy lại danh sách các danh mục và thương hiệu
                ViewBag.Category_id = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.Brand_id = new SelectList(db.Brands.ToList(), "Id", "Name");
                return View(objProduct);
            }
        }
        [HttpGet]
        public ActionResult Details(int id) { 
            var objProduct = db.Products.Where(n=>n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = db.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = db.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            db.Products.Remove(objProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = db.Products.Find(id);

            if (objProduct == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category_id = new SelectList(db.Categories.ToList(), "Id", "Name", objProduct.Category_id);
            ViewBag.Brand_id = new SelectList(db.Brands.ToList(), "Id", "Name", objProduct.Brand_id);

            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            if (objProduct.ImageUpload != null && objProduct.ImageUpload.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                objProduct.Image = fileName;
                string path = Path.Combine(Server.MapPath("~/Content/images/items/"), fileName);
                objProduct.ImageUpload.SaveAs(path);
            }
            db.Entry(objProduct).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
