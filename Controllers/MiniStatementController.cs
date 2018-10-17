using RoyalBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalBank.Controllers
{
    public class MiniStatementController : Controller
    {
        // GET: MiniStatement
        RoyalBankEntities db = new RoyalBankEntities();
        public ActionResult entertxndetails()
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
        public ActionResult entertxndetails(statdetails obj, long AccountId)
        {
            List<usp_ministatement_Result> list = new List<usp_ministatement_Result>();
            list = db.usp_ministatement(AccountId, obj.startDate, obj.endDate.AddDays(1)).ToList();
            return View("PassBook",list);
        }

        public ActionResult PassBook()
        {
            return View();
        }

    }
}