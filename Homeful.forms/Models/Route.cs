using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Firebase.Database;

namespace Homeful.mobile
{
    public class Route : IFirebaseItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, Stop> Stops { get; set; }
        //public ICollection<KeyValuePair<string, Stop>> Stops { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
