using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoyalBank.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult RequestTimeOut()
        {
            return View();
        }

        public ActionResult Conflict()
        {
            return View();
        }

        public ActionResult Gone()
        {
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }

        public ActionResult BadGateway()
        {
            return View();
        }

        public ActionResult ServiceUnavailable()
        {
            return View();
        }

        public ActionResult GatewayTimeOut()
        {
            return View();
        }
    }
}