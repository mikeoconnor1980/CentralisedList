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
        public QueueClientHelper()
        {

        }

        public long MessageCount()
        {
            var namespaceManager = QueueConnector.CreateNamespaceManager();

            // Get the queue, and obtain the message count.
            var queue = namespaceManager.GetQueue(QueueConnector.QueueName);
            return queue.MessageCount;
        }

        public void SendMessage(object messageItem)
        {
            var message = new BrokeredMessage(messageItem);
            QueueConnector.OrdersQueueClient.Send(message);
        }

        
        

    }
}
