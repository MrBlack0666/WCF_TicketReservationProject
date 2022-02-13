using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServiceService
{
    [DataContract]
    public class AddReservationDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int FlightId { get; set; }

        [DataMember]
        public List<AddPersonDTO> People { get; set; }
    }
}