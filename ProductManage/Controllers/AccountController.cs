using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProductManage.Models;

namespace ProductManage.Controllers
{
    
    public class AccountController : Controller
    {

        ADO_ExampleEntities1 entity=new ADO_ExampleEntities1();
        GarageSystemJaipurEntities khush = new GarageSystemJaipurEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            bool userExist= khush.MstUsers.Any(x=>x.MobileNo ==credentials.Mobile && x.PassWord==credentials.Password);
            MstUser u = khush.MstUsers.FirstOrDefault(x => x.MobileNo == credentials.Mobile && x.PassWord == credentials.Password);
            if (userExist)
            {
                FormsAuthentication.SetAuthCookie(u.MobileNo, false);
                return RedirectToAction("Index","Product");
            }
            ModelState.AddModelError("", "ID and Password is Invalid");


            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserLogin userinfo)
        {
           
                if (userinfo != null) {
                    entity.UserLogins.Add(userinfo);
                    entity.SaveChanges();
                }
                else {
                    ModelState.AddModelError("", "Provide Details");
                }
      
            return RedirectToAction("Login");
        }
        
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}