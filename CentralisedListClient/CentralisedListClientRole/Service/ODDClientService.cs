using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ODD.Objects;
using Newtonsoft.Json;

namespace CentralisedListClientRole.Service
{
    public class ODDClientService
    {
        public async Task<List<Client>> GetClientsAsync(
        )

        public List<Client> GetGizmos()
        {
            var uri = System.Web.Util.getServiceUri("Gizmos");
            using (WebClient webClient = new WebClient())
            {
                return JsonConvert.DeserializeObject<List<Client>>(
                    webClient.DownloadString(uri)
                );
            }
        }
    }
}