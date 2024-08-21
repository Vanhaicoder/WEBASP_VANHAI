using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities(); // Di chuyển khai báo biến db vào lớp

        // GET: Admin/User
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            // Xử lý truy vấn tìm kiếm
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            // Truy vấn người dùng theo bộ lọc tìm kiếm
            var listUser = db.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                listUser = listUser.Where(n => n.FirstName.Contains(SearchString) || n.Email.Contains(SearchString));
            }

            // Sắp xếp theo Id giảm dần
            listUser = listUser.OrderByDescending(n => n.Id);

            // Cài đặt phân trang
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(listUser.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User objUser)
        {
            if (ModelState.IsValid)
            {
                if (objUser.ImageUpload != null && objUser.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objUser.ImageUpload.FileName);
                    string extension = Path.GetExtension(objUser.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objUser.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/users/"), fileName);
                    objUser.ImageUpload.SaveAs(path);
                }

                db.Users.Add(objUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objUser);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = db.Users.Find(id);
            if (objUser == null)
            {
                return HttpNotFound();
            }

            return View(objUser);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUser = db.Users.Find(id);
            if (objUser == null)
            {
                return HttpNotFound();
            }

            return View(objUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userToDelete = db.Users.Find(id);
            if (userToDelete != null)
            {
                db.Users.Remove(userToDelete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objUser = db.Users.Find(id);
            if (objUser == null)
            {
                return HttpNotFound();
            }

            return View(objUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User objUser)
        {
            if (ModelState.IsValid)
            {
                if (objUser.ImageUpload != null && objUser.ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objUser.ImageUpload.FileName);
                    string extension = Path.GetExtension(objUser.ImageUpload.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
                    objUser.Image = fileName;
                    string path = Path.Combine(Server.MapPath("~/Content/images/users/"), fileName);
                    objUser.ImageUpload.SaveAs(path);
                }

                db.Entry(objUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(objUser);
        }
    }
}
