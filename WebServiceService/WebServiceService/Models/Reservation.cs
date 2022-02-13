using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServiceService
{
    [DataContract]
    public class Reservation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int FlightId { get; set; }
        [DataMember]
        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }

        [DataMember]
        public virtual ICollection<Person> People { get; set; }
    }
}