using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceBus.Lib
{
    public class QueueClientHelper
    {
        NamespaceManager _namespacemanager;
        public QueueClientHelper()
        {
            _namespacemanager = QueueConnector.CreateNamespaceManager();
        }

        public long ActiveMessageCount()
        {
            return _namespacemanager.GetQueue(QueueConnector.QueueName).MessageCountDetails.ActiveMessageCount;
        }

        public long DeadMessageCount()
        {
            return _namespacemanager.GetQueue(QueueConnector.QueueName).MessageCountDetails.DeadLetterMessageCount;
        }

        public void SendMessage(object messageItem)
        {
            var message = new BrokeredMessage(messageItem);
            QueueConnector.OrdersQueueClient.Send(message);
        }
    }
}
