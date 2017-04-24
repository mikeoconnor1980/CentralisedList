using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using ODD.Objects;
using ODD.Data;
using CentralisedListObjects;
using System.Configuration;

namespace ServiceBus.WorkerClient
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        private string _QueueName;
        private string _EndpointUri;
        private string _PrimaryKey;
        private string _Database;

        QueueClient Client;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            _EndpointUri = CloudConfigurationManager.GetSetting("EndpointUri").ToString();
            _PrimaryKey = CloudConfigurationManager.GetSetting("PrimaryKey").ToString();
            _Database = CloudConfigurationManager.GetSetting("Database").ToString();
            _QueueName = CloudConfigurationManager.GetSetting("QueueName").ToString();

            Trace.WriteLine("_EndpointUri:  " + _EndpointUri);
            Trace.WriteLine("_PrimaryKey:  " + _PrimaryKey);
            Trace.WriteLine("_Database:  " + _Database);

            Trace.WriteLine("Starting processing of messages");
            
            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            Client.OnMessage(async (receivedMessage) =>
                {
                    try
                    {
                        // Process the message
                        Trace.WriteLine("Processing", receivedMessage.SequenceNumber.ToString());
                        // View the message as an OnlineOrder.
                        CentralisedListObjects.Client message = receivedMessage.GetBody<CentralisedListObjects.Client>();

                        ODD.Objects.Client client = new ODD.Objects.Client();
                        client.id = message.id;
                        client.name = message.name;

                        Trace.WriteLine(client.id+ ": " + client.name, "ProcessingMessage");
                        
                        DataCommand _dc = new DataCommand(_EndpointUri, _PrimaryKey, _Database, "OddCollection_dev");
                        ClientData data = new ClientData(_dc, "clients");

                        //UpsertDocumentAsync - does not work!?
                        await data.ReplaceById(client.id, client, true);

                        receivedMessage.Complete();
                        Trace.WriteLine("Message Processed");
                    }
                    catch(Exception ex)
                    {
                        Trace.WriteLine("An error occurred processing message: " + ex.Message);
                    }
                });

            CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Create the queue if it does not exist already
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            Trace.WriteLine("namespaceManager" + namespaceManager);
            if (!namespaceManager.QueueExists(_QueueName))
            {
                namespaceManager.CreateQueue(_QueueName);
            }

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, _QueueName);
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }
    }
}
