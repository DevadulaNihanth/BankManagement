using RoyalBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RoyalBank.Controllers
{
    public class LoginController : Controller
    {
        RoyalBankEntities db = new RoyalBankEntities();
        // GET: Login
        public ActionResult Login()
        {
            login_master objLogin = new login_master();
            objLogin.Roles = db.role_master.ToList<role_master>();
            return View(objLogin);
        }
        [HttpPost]
        public ActionResult Login(login_master objLogin)
        {
            if (ModelState.IsValid)
            {
                var obj = db.login_master.Where(a => a.UserName.Equals(objLogin.UserName) && a.Password.Equals(objLogin.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["login_id"] = objLogin.login_id.ToString();
                    Session["UserName"] = objLogin.UserName.ToString();
                    Session["SessionID"] = DateTime.Now.ToString();
                    if (Session["UserName"].Equals("Accex123"))
                    {
                        Session["role_id"] = "1";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        Session["role_id"] = "2";
                        return RedirectToAction("Cashier_Dashboard");
                    }
                   
                    
                }
                else
                {
                    TempData["notice"] = "Invalid Credentials";
                    return RedirectToAction("Login");
                }
            }
            return View(objLogin);
        }

        public ActionResult Dashboard()
        {
            if (Convert.ToInt32(Session["role_id"]) == 1)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }


        public ActionResult Cashier_Dashboard()
        {
            if ( Convert.ToInt32(Session["role_id"]) == 2)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        
    }
}