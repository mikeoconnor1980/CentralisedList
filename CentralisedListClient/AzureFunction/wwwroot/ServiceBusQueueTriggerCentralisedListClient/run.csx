#r "Newtonsoft.Json"

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static void Run(string myQueueItem, out object outputDocument, TraceWriter log)
{
    log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

    dynamic client = JObject.Parse(myQueueItem);

  outputDocument = new {
    id = client.id,
    name = client.name
  };
}
