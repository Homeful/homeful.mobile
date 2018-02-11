using System;
using Firebase.Database;

namespace Homeful.mobile
{
    public class Stop
    {
        public Camp Camp { get; set; }
        public bool Complete { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
