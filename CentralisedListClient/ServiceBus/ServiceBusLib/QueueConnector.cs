using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;

namespace ServiceBus.Lib
{
    public static class QueueConnector
    {
        // cache rather than recreating it
        public static QueueClient OrdersQueueClient;
        public const string Namespace = "ODDNameSpaceStd";
        public const string QueueName = "ODDTopicClientList";
        public const string _connectionstring = "Endpoint=sb://oddnamespacestd.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=O3GbnJ6BuXuPgJGiGX4PuWdZgzDc2NMsxbp04br/WwY=";

        public static NamespaceManager CreateNamespaceManager()
        {
            // Create the namespace manager which gives you access to
            // management operations.
            return NamespaceManager.CreateFromConnectionString(_connectionstring);

            //var uri = ServiceBusEnvironment.CreateServiceUri("sb", Namespace, String.Empty);
            //var tP = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "O3GbnJ6BuXuPgJGiGX4PuWdZgzDc2NMsxbp04br/WwY=");
            //return new NamespaceManager(uri, tP);
        }

        public static void Initialize()
        {
            // Using Http to be friendly with outbound firewalls.
            ServiceBusEnvironment.SystemConnectivity.Mode =
                ConnectivityMode.Http;

            // Create the namespace manager which gives you access to
            // management operations.
            var namespaceManager = CreateNamespaceManager();

            // Create the queue if it does not exist already.
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Get a client to the queue.
            var messagingFactory = MessagingFactory.Create(
                namespaceManager.Address,
                namespaceManager.Settings.TokenProvider);
            OrdersQueueClient = messagingFactory.CreateQueueClient(QueueName);
        }
    }
}
