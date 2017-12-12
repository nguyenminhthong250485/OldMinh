using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BET_BET
{
    public class getUpdates
    {
        public bool ok { get; set; }
        public result[] result { get; set; }
    }

    public class result
    {
        public string update_id { get; set; }
        public message message { get; set; }
    }

    public class message
    {
        public int message_id { get; set; }
        public objFrom from { get; set; }
        public objChat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public entities[] entities { get; set; }
    }

    public class entities
    {
        public string type { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
    }

    public class objFrom
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
    public class objChat
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        //public string username { get; set; }
        public string type { get; set; }
    }
}
