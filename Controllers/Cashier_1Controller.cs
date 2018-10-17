using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoyalBank.Models;

namespace RoyalBank.Controllers
{
    public class Cashier_1Controller : Controller
    {
        // GET: Transaction_1
        RoyalBankEntities dbo = new RoyalBankEntities();
        public ActionResult ViewAll()
        {
            List<usp_view_account_Result> acc_list = new List<usp_view_account_Result>();
            acc_list = dbo.usp_view_account().ToList();
            return View(acc_list);
        }

        //deposit starts here
        public ActionResult Deposit(long? id)
        {
            usp_view_account_by_accountid_Result acc_details = new usp_view_account_by_accountid_Result();
            acc_details = dbo.usp_view_account_by_accountid(id).FirstOrDefault();
            if(acc_details.Status=="Inactive")
            {
                //pop up this accoint does not exists....inactive
                return RedirectToAction("ViewAll");
            }
            
            return View(acc_details);
        }
        [HttpPost]
        public ActionResult Deposit(usp_view_account_by_accountid_Result acc_details,FormCollection formobj)
        {
            int amount=Convert.ToInt32(Request.Form["amt"]);
            //new balnce is not working...instead i am viewing by id
            decimal new_bal = dbo.usp_addmoney_by_accountid(Convert.ToInt32(acc_details.AccountId), amount);
            this.dbo.SaveChanges();
            //for pop up use these
            acc_details = dbo.usp_view_account_by_accountid(acc_details.AccountId).FirstOrDefault();
            //use acc_details.balance for pop up of new balance

            return RedirectToAction("ViewAll");
        }

        //withdraw starts here
        public ActionResult Withdraw(long? id)
        {   usp_view_account_by_accountid_Result acc_details=new usp_view_account_by_accountid_Result();
            acc_details=dbo.usp_view_account_by_accountid(id).FirstOrDefault();
            if(acc_details.Status=="Inactive")
            {
                //pop for inactive account i.e not exists
                return RedirectToAction("ViewAll");
            }
            TempData["old_bal"] = acc_details.Balance;
            return View(acc_details);
        }
        [HttpPost]
        public ActionResult Withdraw(usp_view_account_by_accountid_Result acc_details,FormCollection formobj)
        {
           decimal old_bal = Convert.ToDecimal(TempData["old_bal"]);
            int amount=Convert.ToInt32(Request.Form["amt"]);
            if(old_bal<amount)
            {
                //pop up .. not sufficient balance...withdrawal amount is more than balance
                return RedirectToAction("ViewAll");
            }
            else
            {
                dbo.usp_withdrawmoney_by_accountid(Convert.ToInt32(acc_details.AccountId), amount);
                this.dbo.SaveChanges();
                acc_details=dbo.usp_view_account_by_accountid(acc_details.AccountId).FirstOrDefault();
                //pop up success with updated balance as acc_details.Balance
                return RedirectToAction("ViewAll");
            }
        }
    }
}