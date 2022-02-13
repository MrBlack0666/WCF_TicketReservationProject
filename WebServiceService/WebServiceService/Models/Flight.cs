using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServiceService
{
    [DataContract]
    public class Flight
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string To_City { get; set; }
        [DataMember]
        public string From_City { get; set; }
        [DataMember]
        public DateTime FlightDepartureDate { get; set; }
        [DataMember]
        public DateTime FlightArrivalDate { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public int NumberOfSeats { get; set; }
        [DataMember]
        public int NumberOfAvaiableSeats { get; set; }

        [DataMember]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}