using RoyalBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalBank.Controllers
{
    public class CashierController : Controller
    {
        // GET: Cashier
        RoyalBankEntities dbo = new RoyalBankEntities();
        public ActionResult ViewAll()
        {
            if (Convert.ToInt32(Session["role_id"]) == 2)
            {
                List<usp_view_account_Result> acc_list = new List<usp_view_account_Result>();
                acc_list = dbo.usp_view_account().ToList();
                return View(acc_list);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        //deposit starts here
        public ActionResult Deposit(long? id)
        {
            if (Convert.ToInt32(Session["role_id"]) == 2)
            {
                usp_view_account_by_accountid_Result acc_details = new usp_view_account_by_accountid_Result();
                acc_details = dbo.usp_view_account_by_accountid(id).FirstOrDefault();

                if (acc_details.Status == "Inactive" || acc_details == null)
                {
                    TempData["notice"] = "Account does not exist";
                    return View();
                    //pop up this accoint does not exists....inactive
                    //return RedirectToAction("ViewAll");
                }

                return View(acc_details);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult Deposit(usp_view_account_by_accountid_Result acc_details, FormCollection formobj)
        {
            decimal amount = Convert.ToInt32(Request.Form["amt"]);
            //new balnce is not working...instead i am viewing by id
            decimal new_bal = dbo.usp_addmoney_by_accountid(Convert.ToInt32(acc_details.AccountId), amount);
            this.dbo.SaveChanges();
            int n1=dbo.usp_insert_transaction_details(acc_details.AccountId,amount,"Credited");
            this.dbo.SaveChanges();
            //for pop up use these
            acc_details = dbo.usp_view_account_by_accountid(acc_details.AccountId).FirstOrDefault();
            //use acc_details.balance for pop up of new balance
            TempData["notice"] = "Deposit Successful. Current Balance:" +acc_details.Balance;
            return View();
        }

        //withdraw starts here
        public ActionResult Withdraw(long? id)
        {
            if (Convert.ToInt32(Session["role_id"]) == 2)
            {
                usp_view_account_by_accountid_Result acc_details = new usp_view_account_by_accountid_Result();
                acc_details = dbo.usp_view_account_by_accountid(id).FirstOrDefault();
                if (acc_details.Status == "Inactive")
                {
                    TempData["notice"] = "Account does not exist";
                    return View();
                    //pop for inactive account i.e not exists
                    //return RedirectToAction("ViewAll");
                }
                TempData["old_bal"] = acc_details.Balance;
                return View(acc_details);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult Withdraw(usp_view_account_by_accountid_Result acc_details, FormCollection formobj)
        {
            decimal old_bal = Convert.ToDecimal(TempData["old_bal"]);
            decimal amount = Convert.ToInt32(Request.Form["amt"]);
            if (old_bal < amount)
            {
                TempData["notice"] = "Your acccount dont have sufficient balance";
                //pop up .. not sufficient balance...withdrawal amount is more than balance
                //return RedirectToAction("ViewAll");
                return View();
            }
            else
            {
                dbo.usp_withdrawmoney_by_accountid(Convert.ToInt32(acc_details.AccountId), amount);
                this.dbo.SaveChanges();
                int n1 = dbo.usp_insert_transaction_details(acc_details.AccountId, amount, "Debited");
                this.dbo.SaveChanges();
                acc_details = dbo.usp_view_account_by_accountid(acc_details.AccountId).FirstOrDefault();
                //pop up success with updated balance as acc_details.Balance
                //return RedirectToAction("ViewAll");
                TempData["notice"] = "Withdraw Successful. Current Balance:" +acc_details.Balance;
                return View();
            }
        }
        //transfer starts here

        public ActionResult Transfer()
        {
            if (Convert.ToInt32(Session["role_id"]) == 2)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public JsonResult GetSorces()
        {

            return Json(dbo.usp_source_accounts().ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSorces1(int id)
        {
            int i = Convert.ToInt32(id);
            return Json(dbo.usp_source_accounts1(i).ToList(), JsonRequestBehavior.AllowGet);
        }
       // public JsonResult GetSource()
       // {
       //     return Json(dbo.usp_source_account().ToList(), JsonRequestBehavior.AllowGet);
       // }
       //public JsonResult GetDestination(int source_id)
       // {
       //     int id = Convert.ToInt32(source_id);
       //     return Json(dbo.usp_destination_account(id).ToList(), JsonRequestBehavior.AllowGet);
       // }
        [HttpPost]
       public ActionResult Transfer(Transfer_manually_added transfer)
        {   
            
            
            usp_view_account_by_type_and_id_Result view_src=new usp_view_account_by_type_and_id_Result();
            usp_view_account_by_type_and_id_Result view_dest=new usp_view_account_by_type_and_id_Result();

            int src1 = Convert.ToInt32(Request.Form["dropdownState"]);
            transfer.SrcAccounType = (src1 == 1) ? "Savings" : "Current";
            int dest1 = Convert.ToInt32(Request.Form["dropdownCity"]);
            transfer.DestAccounType = (dest1 == 1) ? "Savings" : "Current";

            view_src= dbo.usp_view_account_by_type_and_id(transfer.CustomerId,transfer.SrcAccounType).FirstOrDefault();
            view_dest=dbo.usp_view_account_by_type_and_id(transfer.CustomerId,transfer.DestAccounType).FirstOrDefault();
            if(view_src.Status=="Inactive"||view_src==null)
            {   //pop up source account does not exists
                TempData["notice"] = "Source Account does not exist";
                return View();
            }
            else if (view_dest.Status == "Inactive" || view_src == null)
            {
                TempData["notice"] = "Destination Account does not exist";
                //pop up destination account does not exists
                return View();
            }
            else if(view_src.Balance<transfer.Amount)
            {
                TempData["notice"] = "You dont have sufficient balance in source account";
                //pop up Not sufficient amount in source
                return View();
            }
            else
            {
                int src_acc_id = Convert.ToInt32(view_src.AccountId);
                int dest_acc_id = Convert.ToInt32(view_dest.AccountId);
                int s=dbo.usp_withdrawmoney_by_accountid(src_acc_id, transfer.Amount);
                int d=dbo.usp_addmoney_by_accountid(dest_acc_id, transfer.Amount);
                int n1 = dbo.usp_insert_transaction_details(src_acc_id, transfer.Amount, "Debited");
                this.dbo.SaveChanges();
                n1 = dbo.usp_insert_transaction_details(dest_acc_id, transfer.Amount, "Credited");
                this.dbo.SaveChanges();
                //pop up succesfull
                //return RedirectToAction("ViewAll");
                TempData["notice"] = "Transfer Successful";
                return View();
            }



            return View();
        }
    }
}