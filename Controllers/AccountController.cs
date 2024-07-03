using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CinemaWeb.Models;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;




namespace CinemaWeb.Controllers
{

    public class AccountController : Controller
    {
        public QLCinemaWebEntities db = new QLCinemaWebEntities();
        

        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Register(string name, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                ModelState.AddModelError("", "Thông tin đăng ký không hợp lệ! Vui lòng thử lại.");
                return View();
            }
            else if (IsExistedEmail(email))
            {
                ModelState.AddModelError("", "Email đã tồn tại! Vui lòng sử dụng một Email khác.");
                return View();
            }
            else if (password.Length < 6)
            {
                ModelState.AddModelError("", "Mật khẩu quá ngắn! Vui lòng sử dụng mật khẩu dài hơn 6 ký tự.");
                return View();
            }
            else if (!password.Equals(confirmPassword))
            {
                ModelState.AddModelError("", "Mật khẩu nhập lại không trùng khớp! Vui lòng nhập lại.");
                return View();
            }
            else if (name.Length > 5 && ContainsSpecialCharacters(name))
            {
                ModelState.AddModelError("", "Tên không được chứa ký tự đặc biệt hoặc tên quá ngắn.");
                return View();
            }
            
            else if (!IsValidEmail(email))
            {
                ModelState.AddModelError("", "Email không hợp lệ! Vui lòng sử dụng một Email hợp lệ.");
                return View();
            }
            else
            {
                Account account = new Account();
                account.email = email;
                account.password = password;
                account.role = 0;
                account.name = name;

                string hashedPassword = BCryptNet.HashPassword(account.password);
                account.password = hashedPassword;
                db.Accounts.Add(account);
                db.SaveChanges();

                return RedirectToAction("Login", "Account");
            }
        }

        // Kiểm tra xem email có hợp lệ không
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        // Kiểm tra xem email có tồn tại không

        private bool IsExistedEmail(string email)
        {
            var account = db.Accounts.FirstOrDefault(a => a.email == email);

            return (account != null) ? true : false;
        }
        // Kiểm tra các ký tự đặc biệt
        private static bool ContainsSpecialCharacters(string text)
        {
            // Danh sách các ký tự đặc biệt
            const string specialCharacters = "!@#$%^&*()_+{}|:\"<>?,./";

            // Kiểm tra xem text có chứa các ký tự đặc biệt hay không
            for (int i = 0; i < text.Length; i++)
            {
                if (specialCharacters.Contains(text[i]))
                {
                    return true;
                }
            }

            return false;
        }
      
        public ActionResult Login()
        {
         
            return View();
        }
        // Khi login thành công nếu như account là custommer thì layout signin/ signup sẽ thay đổi thành tên người dùng và sẽ có hiển thị các chức năng của custommer
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Account customer = IsValidCustomer(email, password);
            if (customer != null)
            {
                Session["Customer"] = customer;
                return RedirectToAction("Index", "Home");
            }
            else if (IsValidAdmin(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Thông tin đăng nhập không hợp lệ! Vui lòng thử lại.");
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        private Account IsValidCustomer(string email, string password)
        {
            var customer = db.Accounts.FirstOrDefault(a => a.email == email && a.role == 0);

            if (customer != null && BCryptNet.Verify(password, customer.password))
            {
                return customer;
            }

            return null;
        }

        private bool IsValidAdmin(string email, string password)
        {
            var admin = db.Accounts.FirstOrDefault(a => a.email == email && a.role == 1);

            return (admin != null && BCryptNet.Verify(password, admin.password));
        }

       
    }
}