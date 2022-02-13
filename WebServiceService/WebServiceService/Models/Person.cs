using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServiceService
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public int ReservationId { get; set; }
        [DataMember]
        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; }
    }
}