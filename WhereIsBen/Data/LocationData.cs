using System.Text.Json.Serialization;

namespace WhereIsBen.Data
{
    public class Location
    {
        public int Id { get; set; }
        public string location { get; set; }
    }

    public class LocationData
    {
        public List<Location> locations { get; set; }
    }
}