using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODD.Data;
using System.Threading.Tasks;
using ODD.Objects;
using System.Configuration;

namespace CentralisedListClientRole.Controllers
{
    public class ODDClientController : Controller
    {        
        // GET: ODDClient
        public async Task<ActionResult> Product1()
        {
            string EndpointUri = ConfigurationManager.AppSettings["DocumentDBEndpointUri"].ToString();
            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"].ToString();
            ViewBag.SyncOrAsync = "Asynchronous";
            DataCommand _dc = new DataCommand(EndpointUri, PrimaryKey, "odd_main_dev");
            ClientData data = new ClientData(_dc, "clients");
            List<Client> clients = await data.Load();

            return View(clients);
        }

        public async Task<ActionResult> Product2()
        {
            string EndpointUri = ConfigurationManager.AppSettings["DocumentDBEndpointUri"].ToString();
            string PrimaryKey = ConfigurationManager.AppSettings["DocumentDBPrimaryKey"].ToString();
            ViewBag.SyncOrAsync = "Asynchronous";
            DataCommand _dc = new DataCommand(EndpointUri, PrimaryKey, "odd_main_dev");
            ClientData data = new ClientData(_dc, "clients2");
            List<Client> clients = await data.Load();

            return View(clients);
        }



        // GET: ODDClient/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ODDClient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ODDClient/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ODDClient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ODDClient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ODDClient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ODDClient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
