using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ODD.Objects
{
    public class Obligation
    {
        [JsonProperty(PropertyName = "id")]
        public string obligationId { get; set; }
        public string obligationName { get; set; }
        public string obligationPriorId { get; set; }
        public Client[] clients { get; set; }
        public string processType { get; set; }
        public Jurisdiction jursidiction { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
