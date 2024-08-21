using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{
    public class PaymentController : Controller
    {
        WebBanHangEntities db = new WebBanHangEntities();

        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];

                // Gán dữ liệu cho Order
                Order objOrder = new Order();
                objOrder.Delivery_name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.User_id = int.Parse(Session["idUser"].ToString());
                objOrder.Delivery_email = Session["Email"].ToString();
                objOrder.Delivery_phone = Session["Phone"].ToString();
                objOrder.Delivery_address = Session["Address"].ToString();
                objOrder.Status = 1;
                db.Orders.Add(objOrder);

                db.SaveChanges();

                long intOrderId = objOrder.Id;
                List<Orderdetail> lstOrderdetail = new List<Orderdetail>();

                foreach (var item in lstCart)
                {
                    Orderdetail obj = new Orderdetail();
                    obj.Qty = item.Quantity;
                    obj.Order_id = intOrderId;
                    obj.Product_id = item.Product.Id;

                    // Tính giá và thành tiền
                    float price = item.Sale_price > 0 ? item.Sale_price : item.Price;
                    obj.Price = price;
                    obj.Amount = price * item.Quantity;

                    lstOrderdetail.Add(obj);
                }

                db.Orderdetails.AddRange(lstOrderdetail);
                db.SaveChanges();

                // Xóa giỏ hàng
                Session["cart"] = null;

                // Truyền danh sách các sản phẩm đã mua vào view
                ViewBag.PurchasedProducts = lstCart;

                return View();
            }
        }



    }
}
