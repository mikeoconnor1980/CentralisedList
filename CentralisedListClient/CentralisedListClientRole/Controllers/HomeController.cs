using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CentralisedListObjects;
using ServiceBus.Lib;

namespace CentralisedListClientRole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Simply redirect to Submit, since Submit will serve as the
            // front page of this application.
            return RedirectToAction("Submit");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        // GET: /Home/Submit.
        // Controller method for a view you will create for the submission
        // form.
        public ActionResult Submit()
        {
            QueueClientHelper qc = new QueueClientHelper();
            ViewBag.ActiveMessageCount = qc.ActiveMessageCount();
            ViewBag.DeadMessageCount = qc.DeadMessageCount();

            return View();
            // Get a NamespaceManager which allows you to perform management and
            // diagnostic operations on your Service Bus queues.
        }

        // POST: /Home/Submit.
        // Controller method for handling submissions from the submission
        // form.
        [HttpPost]
        // Attribute to help prevent cross-site scripting attacks and
        // cross-site request forgery.  
        [ValidateAntiForgeryToken]
        public ActionResult Submit(Client client)
        {
            if (ModelState.IsValid)
            {
                // Will put code for submitting to queue here.
                QueueClientHelper qc = new QueueClientHelper();
                qc.SendMessage(client);
                return RedirectToAction("Submit");
            }
            else
            {
                return View(client);
            }
        }
    }
}