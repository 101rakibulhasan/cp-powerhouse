using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cp_powerhouse.Models.ListObjects
{
    public class Resource
    {
        public string icon { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class CListItem
    {
        public int duration { get; set; }
        public string end { get; set; }
        public string @event { get; set; }
        public string href { get; set; }
        public int id { get; set; }
        public Resource resource { get; set; }
        public string start { get; set; }
    }

    public class Meta
    {
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total_count { get; set; }
    }

    public class Root
    {
        public Meta meta { get; set; }
        public List<CListItem> objects { get; set; }
    }
}
