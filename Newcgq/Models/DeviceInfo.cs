using System;
using System.Collections.Generic;

namespace Newcgq.Models
{
    public partial class DeviceInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DeviceId { get; set; }
        public bool Admoduel { get; set; }
        public bool Damoduel { get; set; }
        public bool Iomoduel { get; set; }

        public UserInfo UserNameNavigation { get; set; }
    }
}
