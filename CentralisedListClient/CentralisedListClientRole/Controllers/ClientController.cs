using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CentralisedListData;
using CentralisedListObjects;
using ServiceBus.Lib;
using Newtonsoft.Json;

namespace CentralisedListClientRole.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ClientConnection"].ConnectionString;
            ClientData cd = new ClientData(connectionstring);
            List<Client> clients = cd.Load();

            TopicClientHelper qc = new TopicClientHelper();
            ViewBag.ActiveMessageCount = qc.ActiveMessageCount();
            ViewBag.DeadMessageCount = qc.DeadMessageCount();

            return View(clients);
        }

        // GET: Client/Details/5
        public ActionResult Details(string id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ClientConnection"].ConnectionString;
            ClientData cd = new ClientData(connectionstring);
            Client c = cd.GetClient(id);
            return View(c);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client client)
        {
            try
            {
                string connectionstring = ConfigurationManager.ConnectionStrings["ClientConnection"].ConnectionString;
                ClientData cd = new ClientData(connectionstring);
                cd.Insert(client);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(string id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ClientConnection"].ConnectionString;
            ClientData cd = new ClientData(connectionstring);
            Client c = cd.GetClient(id);
            return View(c);
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Client client)
        {
            try
            {
                string connectionstring = ConfigurationManager.ConnectionStrings["ClientConnection"].ConnectionString;
                ClientData cd = new ClientData(connectionstring);
                cd.Update(id, client);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult SubmitToServiceBus(string id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ClientConnection"].ConnectionString;
            ClientData cd = new ClientData(connectionstring);
            Client client = cd.GetClient(id);

            QueueClientHelper queueClientHelper = new QueueClientHelper();

            string message = JsonConvert.SerializeObject(client);
            string messageBasic = "{ \"id\" : \"" + client.id + "\", \"name\" : \"" + client.name + "\"}";

            queueClientHelper.SendMessage(messageBasic);

            cd.ResetDirtyFlag(id);

            return RedirectToAction("Index");
        }

        public ActionResult SubmitToServiceBusTopic(string id)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ClientConnection"].ConnectionString;
            ClientData cd = new ClientData(connectionstring);
            Client client = cd.GetClient(id);

            TopicClientHelper topicClientHelper = new TopicClientHelper();
            //topicClientHelper.CreateTopicSubscription("ODDTopicClientList", "ODDTopicClientListSubscription");

            string message = JsonConvert.SerializeObject(client);
            string messageBasic = "{ \"id\" : \"" + client.id + "\", \"name\" : \"" + client.name + "\"}";

            topicClientHelper.SendMessage(messageBasic);

            cd.ResetDirtyFlag(id);

            return RedirectToAction("Index");
        }
    }
}
