using PagedList; // Đảm bảo bạn đã thêm thư viện PagedList
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();

        // GET: Order
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

            ViewBag.CurrentFilter = searchString;

            var orders = db.Orders.Include("User").AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.Delivery_name.Contains(searchString) ||
                                            o.Delivery_email.Contains(searchString) ||
                                            o.Delivery_phone.Contains(searchString));
            }

            orders = orders.OrderByDescending(o => o.Id);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var orderdetail = db.Orderdetails.FirstOrDefault(od => od.Order_id == id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
