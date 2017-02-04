using System;
using System.Collections.Generic;

namespace France_Vacances.Model
{
    public class AccommodationModel
    {
        public string AccommodationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public byte Rooms { get; set; }
        public byte Persons { get; set; }
        public List<string> Facilities { get; set; }
        public List<string> Images { get; set; }
        public double ReviewScore { get; set; }
        public string Stars { get; set; }
        public List<DateTimeOffset> BookedDays { get; set; }
    }
}
