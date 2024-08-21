using WEBTHAY_LEVANHAI_2122110022.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList; // Thêm namespace cho PagedList
using System.Drawing.Printing;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{
    public class CategoryController : Controller
    {
        // Khởi tạo đối tượng DbContext
        private readonly WebBanHangEntities db = new WebBanHangEntities();

        // GET: Category
        public ActionResult Index()
        {
            var listCategory = db.Categories.ToList();
            return View(listCategory);
        }

        // GET: ProductByCategoryId/5
        public ActionResult ProductByCategoryId(int id, int page = 1, int pageSize = 8)
        {
            // Đếm tổng số sản phẩm của thương hiệu cụ thể
            var totalProducts = db.Products.Where(p => p.Category_id == id).Count();

            // Lọc và phân trang sản phẩm theo BrandId
            var products = db.Products
                .Where(p => p.Category_id == id)
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
