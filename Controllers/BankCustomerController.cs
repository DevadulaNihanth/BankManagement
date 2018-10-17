using RoyalBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalBank.Controllers
{
    public class BankCustomerController : Controller
    {
        RoyalBankEntities db = new RoyalBankEntities();
        // GET: BankCustomer
        public ActionResult CustomerList()
        {
            if (Convert.ToInt32(Session["role_id"]) == 1)
            {
                List<usp_view_customer_Result> customerList = new List<usp_view_customer_Result>();
                customerList = db.usp_view_customer().ToList();
                return View(customerList);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        
        }

        public ActionResult AddCustomer()
        {
            if(Convert.ToInt32(Session["role_id"])==1)
            { 
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
            
        }
        public JsonResult GetStates()
        {

            return Json(db.usp_state_dropdown().ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCitiesByStateId(int state_id)
        {
            int id = Convert.ToInt32(state_id);

            return Json(db.usp_city_dropdown(id).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddCustomer(customer_details objCustomer, FormCollection objForm, long SSNID)
        {
            int saveVal=0;
            if(ModelState.IsValid)
            {
                ObjectParameter objParam = new ObjectParameter("Id_out", typeof(int));
                objCustomer.State = objForm["dropdownState"];
                objCustomer.City = objForm["dropdownCity"];
                objCustomer.Status = "Active";
                int save = db.usp_save_customer(objCustomer.CustomerId, SSNID, objCustomer.CustomerName, objCustomer.DOB, objCustomer.Address1, objCustomer.Address2, objCustomer.City, objCustomer.State, objCustomer.Status, objParam);
                this.db.SaveChanges();
                saveVal = Convert.ToInt32(objParam.Value);

                
               
                    TempData["notice"] = "Customer added successfully with Customer ID:" + saveVal;
                    ModelState.Clear();
                
                //return RedirectToAction("CustomerList");
                return View();

            }
            
            return View(objCustomer);
        }

        public ActionResult EditCustomer(int? id)
        {
            if (Convert.ToInt32(Session["role_id"]) == 1)
            {
                usp_view_by_customerid_Result objCustomer = new usp_view_by_customerid_Result();
                if (id != null)
                {
                    objCustomer = db.usp_view_by_customerid(id).FirstOrDefault();
                }
                return View(objCustomer);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult EditCustomer(usp_view_by_customerid_Result objCustomer, FormCollection objForm)
        {
            ObjectParameter objParam = new ObjectParameter("Id_out", typeof(int));
            objCustomer.State = objForm["dropdownState"];
            objCustomer.City = objForm["dropdownCity"];
            objCustomer.Status = "Active";
            int save = db.usp_save_customer(objCustomer.CustomerId, objCustomer.SSNID, objCustomer.CustomerName, objCustomer.DOB, objCustomer.Address1, objCustomer.Address2, objCustomer.City, objCustomer.State, objCustomer.Status, objParam);
            this.db.SaveChanges();
            int saveVal = Convert.ToInt32(objParam.Value);
            //return RedirectToAction("CustomerList");
            TempData["notice"] = "Customer updated successfully";
            ModelState.Clear();
            return View();
                
        }

        public ActionResult DeleteCustomer(int? id)
        {
            if (Convert.ToInt32(Session["role_id"]) == 1)
            {
                usp_view_by_customerid_Result objCustomer = new usp_view_by_customerid_Result();
                if (id != null)
                {
                    objCustomer = db.usp_view_by_customerid(id).FirstOrDefault();
                    TempData["id"] = id;
                  
                }
                return View(objCustomer);
                
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           
        }
        [HttpPost]
        public ActionResult DeleteCustomer(usp_view_by_customerid_Result objCustomer)
        {
            int result = db.usp_delete_customerbyid(Convert.ToInt32(TempData["id"]));
            this.db.SaveChanges();
            if (result == -1)
            {
                TempData["notice"] = "Customer is associated with active account";
            }
            else
            {
                TempData["notice"] = "Customer is removed successfully";
            }
            return View();
           
        }

    }
}