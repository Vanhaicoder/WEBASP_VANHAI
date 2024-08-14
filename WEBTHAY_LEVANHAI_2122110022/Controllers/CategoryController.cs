using WEBTHAY_LEVANHAI_2122110022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        WebBanHangEntities db = new WebBanHangEntities();
        public ActionResult Index()
        {
            var listCategory = db.Categories.ToList();
            return View(listCategory);
        }

        public ActionResult ProductByCategoryId(int id)
        {
            var ListProduct = db.Products.Where(n => n.Category_id == id).ToList();
            return View(ListProduct);
        }
    }
}
