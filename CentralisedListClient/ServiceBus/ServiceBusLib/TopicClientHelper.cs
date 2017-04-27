using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Lib
{
    public class TopicClientHelper
    {
        NamespaceManager _namespacemanager;
        public TopicClientHelper()
        {
            _namespacemanager = TopicConnector.CreateNamespaceManager();
        }

        public long ActiveMessageCount()
        {
            return _namespacemanager.GetTopic(TopicConnector.TopicName).MessageCountDetails.ActiveMessageCount;
        }

        public long DeadMessageCount()
        {
            return _namespacemanager.GetTopic(TopicConnector.TopicName).MessageCountDetails.DeadLetterMessageCount;
        }

        public void SendMessage(object messageItem)
        {
            var message = new BrokeredMessage(messageItem);
            TopicConnector.topicClient.Send(message);
        }

        private void CreateTopic(string topicName)
        {
            //"ODDTopicClientList"
            if (!_namespacemanager.TopicExists(topicName))
            {
                _namespacemanager.CreateTopic(topicName);
            }
        }

        public void CreateTopicSubscription(string topicName, string subscriptionName)
        {
            this.CreateTopic(topicName);
            this.CreateSubscription(topicName);
        }

        public void CreateSubscription(string subscriptionName)
        {
            //"ODDTopicClientList"
            if (!_namespacemanager.SubscriptionExists(subscriptionName, "AllMessages"))
            {
                _namespacemanager.CreateSubscription(subscriptionName, "AllMessages");
            }
        }
    }
}
