using System;

namespace France_Vacances.Model
{
    public class BookingModel
    {
        public long BookingId { get; set; }
        public string AccommodationId { get; set; }
        public string BookerFirstName { get; set; }
        public string BookerLastName { get; set; }
        public DateTimeOffset BookerBirthDate { get; set; }
        public string BookerEmailAddress { get; set; }
        public string BookerPhoneNumber { get; set; }
        public string BookerAddress { get; set; }
        public string BookerCity { get; set; }
        public string BookerPostalCode { get; set; }
        public string BookerCountry { get; set; }
        public double Price { get; set; }
        public DateTimeOffset BookingStartDateTime { get; set; }
        public DateTimeOffset BookingEndDateTime { get; set; }
    }
}
