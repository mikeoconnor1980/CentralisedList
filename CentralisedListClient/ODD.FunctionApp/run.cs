using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs.Host;
using ODD.Objects;

namespace ODD.FunctionApps
{
    public class DocumentDBClientSyncFn
    {
        public static void Run(string message, out Client outputDocument, TraceWriter log)
        {
            try
            {
                log.Info("Message: " + message);
                dynamic client = JObject.Parse(message);

                outputDocument = new Client
                {
                    id = client.id,
                    name = client.name
                };
            }
            catch(Exception ex)
            {
                log.Error("Error occurred: " + ex.Message);
                outputDocument = null;
            }
            
        }
    }
}
