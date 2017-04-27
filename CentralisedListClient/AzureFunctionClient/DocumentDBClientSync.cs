using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs.Host;
using ODD.Objects;

namespace ODDFunctionApps
{
    public class DocumentDBClientSync
    {
        public static void Run(string message, out Client outputDocument, TraceWriter log)
        {
            dynamic client = JObject.Parse(message);

            log.Info("Test");

            outputDocument = new Client
            {
                id = client.id,
                name = client.name

            };
        }
    }
}
