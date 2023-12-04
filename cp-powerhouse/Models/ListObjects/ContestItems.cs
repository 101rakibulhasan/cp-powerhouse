using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cp_powerhouse.Models
{
    public class ContestItems
    {
        public int id { get; set; }
        public string? platform { get; set; }
        public string? name { get; set; }
        public int startTimeSeconds { get; set; }
        public string? startTimeSecondsView { get; set; }
        public int durationSeconds { get; set; }
        public string? durationSecondsView { get; set; }
        public string? phase { get; set; }
        public string? link { get; set; }

        //
        public int starttime { get; set; }
        public int endtime { get; set; }
    }
}
