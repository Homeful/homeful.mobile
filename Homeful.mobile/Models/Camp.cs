using System;
namespace Homeful.mobile
{
    public class Camp
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
    }
    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
