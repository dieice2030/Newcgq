using System;
using System.Collections.Generic;

namespace Newcgq.Models
{
    public partial class ControllerMessage
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DeviceInfo { get; set; }
        public string Func { get; set; }
        public string Data { get; set; }
        public int Interval { get; set; }
        public int IntervalUnit { get; set; }
        public DateTime? Time { get; set; }
        public string Backup1 { get; set; }
    }
}
