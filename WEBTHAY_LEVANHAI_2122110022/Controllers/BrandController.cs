using WEBTHAY_LEVANHAI_2122110022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{
    public class BrandController : Controller
    {
        // Khởi tạo đối tượng DbContext
        private readonly WebBanHangEntities db = new WebBanHangEntities();

        // GET: Brand
        public ActionResult Index()
        {
            // Lấy danh sách tất cả các thương hiệu từ cơ sở dữ liệu
            var listBrand = db.Brands.ToList();
            // Trả về view cùng với danh sách thương hiệu
            return View(listBrand);
        }

        // GET: Brand/ProductByBrandId/5
        public ActionResult ProductByBrandId(int id, int page = 1, int pageSize = 8)
        {
            // Đếm tổng số sản phẩm của thương hiệu cụ thể
            var totalProducts = db.Products.Where(p => p.Brand_id == id).Count();

            // Lọc và phân trang sản phẩm theo BrandId
            var products = db.Products
                .Where(p => p.Brand_id == id)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            HomeModel model = new HomeModel
            {
                ListCategory = db.Categories.ToList(),
                ListBrand = db.Brands.ToList(),
                ListProduct = products
            };

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;

            return View(model);
        }

    }
}
