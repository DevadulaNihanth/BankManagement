using RoyalBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalBank.Controllers
{
    public class BankAccountController : Controller
    {
        RoyalBankEntities db = new RoyalBankEntities();
        // GET: BankAccount
        public ActionResult AccountList()
        {
            if (Convert.ToInt32(Session["role_id"]) == 1 || Convert.ToInt32(Session["role_id"]) == 2)
            {
                List<usp_view_account_Result> accList = new List<usp_view_account_Result>();
                accList = db.usp_view_account().ToList();
                return View(accList);
            }
            return View();
        }

        public ActionResult AddAccount()
        {
            if (Convert.ToInt32(Session["role_id"]) == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }


        }
        [HttpPost]
        public ActionResult AddAccount(account_details objAccount, int CustomerId)
        {
            if (ModelState.IsValid)
            {
                ObjectParameter objParam = new ObjectParameter("Id_Output", typeof(int));
                int insert = db.usp_insert_account(objAccount.AccountId, CustomerId, objAccount.AccountType, objAccount.Balance, objParam);
                this.db.SaveChanges();
                int insertVal = Convert.ToInt32(objParam.Value);
                if (insertVal == -1)
                {
                    TempData["notice"] = "Account Already Exists";
                    ModelState.Clear();
                    return View();
                }
                else if (insertVal == -2)
                {
                    TempData["notice"] = "Customer does not exist";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    TempData["notice"] = "Account added successfully with Account ID: " +insertVal;
                    ModelState.Clear();
                    return View();
                }
            }
            return View(objAccount);
        }
        public ActionResult Delete()
        {
            if (Convert.ToInt32(Session["role_id"]) == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        [HttpPost]
        public ActionResult Delete(account_details acc_obj)
        {
            TempData["id"] =acc_obj.AccountId;
            return RedirectToAction("ConfirmDelete");
        }
        public ActionResult ConfirmDelete()
        {
            long id = Convert.ToInt64(TempData["id"]);
            usp_view_account_by_accountid_Result acc = new usp_view_account_by_accountid_Result();
            acc = db.usp_view_account_by_accountid(id).FirstOrDefault();
            return View(acc);
            //int result = db.usp_delete_account_by_accountid(id);
            //if (result == -1)
            //{
            //    return RedirectToAction("DeleteAccount");
            //}
            //else
            //{
            //    this.db.SaveChanges();
            //    return View(objAccount);
            //}

        }
        [HttpPost]
        public ActionResult ConfirmDelete(usp_view_account_by_accountid_Result objAccount)
        {
            int result = db.usp_delete_account_by_accountid(objAccount.AccountId);
            if (result == -1)
            {
                TempData["notice"] = "Account cannot be deleted as it has some balance";
                return View();
            }
            else
            {
                this.db.SaveChanges();
                return RedirectToAction("AccountList");
            }


        }

      



    }
}