using System;
using System.Collections.Generic;
namespace Homeful.mobile
{
    public class Route : IFirebaseItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Stop> Stops { get; set; } = new List<Stop>();
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
