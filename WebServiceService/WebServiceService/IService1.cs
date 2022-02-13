using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebServiceService.PasswordAttributes;

namespace WebServiceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Flight> GetFlights();

        [OperationContract]
        Flight AddFlight(AddEditFlightDTO flight);

        [OperationContract]
        bool EditFlight(AddEditFlightDTO flight);

        [OperationContract]
        bool DeleteFlight(int flightId);

        [OperationContract]
        Reservation GetReservationById(int reservationId);

        [OperationContract]
        int AddReservation(AddReservationDTO reservationDTO);

        [OperationContract]
        bool DeleteReservationId(int reservationId);

        [OperationContract]
        byte[] GetReservationConfirmationPdf(int reservationId);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
