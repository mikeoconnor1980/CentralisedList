using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralisedListObjects
{
    public class Client
    {
        public string id { get; set; }
        public string name { get; set; }

        public Boolean is_dirty { get; set; }
    }
}
