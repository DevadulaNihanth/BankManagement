using RoyalBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalBank.Controllers
{
    public class Search_a_Controller : Controller
    {
        RoyalBankEntities db = new RoyalBankEntities();
        // GET: Search_a_
        public ActionResult Search()
        {
            if (Convert.ToInt32(Session["role_id"]) != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult Search(customer_details cus)
        {
            if(cus.CustomerId!=0)
            {
                List<usp_view_by_customerid_or_ssnid_Result> ob = new List<usp_view_by_customerid_or_ssnid_Result>();
                //ob = db.usp_view_by_customerid(cus.CustomerId).ToList();
                long c=0;
                //customer_details c_details = new customer_details();
                ob = db.usp_view_by_customerid_or_ssnid(cus.CustomerId,c).ToList();
                return View("dis",ob);
            }
            else if(cus.SSNID!=0)
            {
                Int32 c = 0;
                List<usp_view_by_customerid_or_ssnid_Result> ssn = new List<usp_view_by_customerid_or_ssnid_Result>();
                ssn = db.usp_view_by_customerid_or_ssnid(c,cus.SSNID).ToList();
                return View("dis", ssn);
            }
            return View();
        }


        public ActionResult dis()
        {
            return View();
        }


        public ActionResult SearchAcc()
        {

            if (Convert.ToInt32(Session["role_id"]) != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult SearchAcc(account_details acc)
        {
            if (acc.CustomerId != 0)
            {
                List<usp_viewAccount_by_customerid_or_Accountid_Result> ob = new List<usp_viewAccount_by_customerid_or_Accountid_Result>();
                //ob = db.usp_view_by_customerid(cus.CustomerId).ToList();
                long c = 0;
                //customer_details c_details = new customer_details();
                ob = db.usp_viewAccount_by_customerid_or_Accountid(acc.CustomerId, c).ToList();
                return View("disp", ob);
            }
            else if (acc.AccountId!= 0)
            {
                List<usp_viewAccount_by_customerid_or_Accountid_Result> ob = new List<usp_viewAccount_by_customerid_or_Accountid_Result>();
                ob = db.usp_viewAccount_by_customerid_or_Accountid(0,acc.AccountId).ToList();
                return View("disp",ob);
            }
            return View();
        }


        public ActionResult disp()
        {
            return View();
        }



    }
}