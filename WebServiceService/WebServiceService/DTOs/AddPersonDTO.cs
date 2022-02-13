using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServiceService
{
    [DataContract]
    public class AddPersonDTO
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
    }
}