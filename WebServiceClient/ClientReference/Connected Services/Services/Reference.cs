﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Services
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Flight", Namespace="http://schemas.datacontract.org/2004/07/WebServiceService")]
    public partial class Flight : object
    {

        private System.DateTime FlightArrivalDateField;
        
        private System.DateTime FlightDepartureDateField;
        
        private string From_CityField;
        
        private int IdField;
        
        private int NumberOfAvaiableSeatsField;
        
        private int NumberOfSeatsField;
        
        private decimal PriceField;
        
        private System.Collections.Generic.List<Services.Reservation> ReservationsField;
        
        private string To_CityField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FlightArrivalDate
        {
            get
            {
                return this.FlightArrivalDateField;
            }
            set
            {
                this.FlightArrivalDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FlightDepartureDate
        {
            get
            {
                return this.FlightDepartureDateField;
            }
            set
            {
                this.FlightDepartureDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string From_City
        {
            get
            {
                return this.From_CityField;
            }
            set
            {
                this.From_CityField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfAvaiableSeats
        {
            get
            {
                return this.NumberOfAvaiableSeatsField;
            }
            set
            {
                this.NumberOfAvaiableSeatsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfSeats
        {
            get
            {
                return this.NumberOfSeatsField;
            }
            set
            {
                this.NumberOfSeatsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price
        {
            get
            {
                return this.PriceField;
            }
            set
            {
                this.PriceField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Services.Reservation> Reservations
        {
            get
            {
                return this.ReservationsField;
            }
            set
            {
                this.ReservationsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string To_City
        {
            get
            {
                return this.To_CityField;
            }
            set
            {
                this.To_CityField = value;
            }
        }

        public string FlightDepartureDateString
        {
            get
            {
                return (FlightDepartureDate.ToShortDateString() + " " + FlightDepartureDate.ToShortTimeString()).Replace('-', '/');
            }
        }

        public string FlightArrivalDateString
        {
            get
            {
                return (FlightArrivalDate.ToShortDateString() + " " + FlightArrivalDate.ToShortTimeString()).Replace('-', '/');
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Reservation", Namespace="http://schemas.datacontract.org/2004/07/WebServiceService")]
    public partial class Reservation : object
    {
        
        private Services.Flight FlightField;
        
        private int FlightIdField;
        
        private int IdField;
        
        private System.Collections.Generic.List<Services.Person> PeopleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Services.Flight Flight
        {
            get
            {
                return this.FlightField;
            }
            set
            {
                this.FlightField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FlightId
        {
            get
            {
                return this.FlightIdField;
            }
            set
            {
                this.FlightIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Services.Person> People
        {
            get
            {
                return this.PeopleField;
            }
            set
            {
                this.PeopleField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Person", Namespace="http://schemas.datacontract.org/2004/07/WebServiceService")]
    public partial class Person : object
    {
        
        private System.DateTime BirthDateField;
        
        private string FirstNameField;
        
        private int IdField;
        
        private Services.Reservation ReservationField;
        
        private int ReservationIdField;
        
        private string SurnameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime BirthDate
        {
            get
            {
                return this.BirthDateField;
            }
            set
            {
                this.BirthDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Services.Reservation Reservation
        {
            get
            {
                return this.ReservationField;
            }
            set
            {
                this.ReservationField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ReservationId
        {
            get
            {
                return this.ReservationIdField;
            }
            set
            {
                this.ReservationIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Surname
        {
            get
            {
                return this.SurnameField;
            }
            set
            {
                this.SurnameField = value;
            }
        }

        public string BirthDateString
        {
            get
            {
                return BirthDate.ToShortDateString().Replace('-', '/');
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AddEditFlightDTO", Namespace="http://schemas.datacontract.org/2004/07/WebServiceService")]
    public partial class AddEditFlightDTO : object
    {
        
        private System.DateTime FlightArrivalDateField;
        
        private System.DateTime FlightDepartureDateField;
        
        private string From_CityField;
        
        private int IdField;
        
        private int NumberOfSeatsField;
        
        private decimal PriceField;
        
        private string To_CityField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FlightArrivalDate
        {
            get
            {
                return this.FlightArrivalDateField;
            }
            set
            {
                this.FlightArrivalDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FlightDepartureDate
        {
            get
            {
                return this.FlightDepartureDateField;
            }
            set
            {
                this.FlightDepartureDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string From_City
        {
            get
            {
                return this.From_CityField;
            }
            set
            {
                this.From_CityField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfSeats
        {
            get
            {
                return this.NumberOfSeatsField;
            }
            set
            {
                this.NumberOfSeatsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price
        {
            get
            {
                return this.PriceField;
            }
            set
            {
                this.PriceField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string To_City
        {
            get
            {
                return this.To_CityField;
            }
            set
            {
                this.To_CityField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AddReservationDTO", Namespace="http://schemas.datacontract.org/2004/07/WebServiceService")]
    public partial class AddReservationDTO : object
    {
        
        private int FlightIdField;
        
        private int IdField;
        
        private System.Collections.Generic.List<Services.AddPersonDTO> PeopleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FlightId
        {
            get
            {
                return this.FlightIdField;
            }
            set
            {
                this.FlightIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Services.AddPersonDTO> People
        {
            get
            {
                return this.PeopleField;
            }
            set
            {
                this.PeopleField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AddPersonDTO", Namespace="http://schemas.datacontract.org/2004/07/WebServiceService")]
    public partial class AddPersonDTO : object
    {
        
        private System.DateTime BirthDateField;
        
        private string FirstNameField;
        
        private string SurnameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime BirthDate
        {
            get
            {
                return this.BirthDateField;
            }
            set
            {
                this.BirthDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Surname
        {
            get
            {
                return this.SurnameField;
            }
            set
            {
                this.SurnameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Services.IService1")]
    public interface IService1
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetFlights", ReplyAction="http://tempuri.org/IService1/GetFlightsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Services.Flight>> GetFlightsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddFlight", ReplyAction="http://tempuri.org/IService1/AddFlightResponse")]
        System.Threading.Tasks.Task<Services.Flight> AddFlightAsync(Services.AddEditFlightDTO flight);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EditFlight", ReplyAction="http://tempuri.org/IService1/EditFlightResponse")]
        System.Threading.Tasks.Task<bool> EditFlightAsync(Services.AddEditFlightDTO flight);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteFlight", ReplyAction="http://tempuri.org/IService1/DeleteFlightResponse")]
        System.Threading.Tasks.Task<bool> DeleteFlightAsync(int flightId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetReservationById", ReplyAction="http://tempuri.org/IService1/GetReservationByIdResponse")]
        System.Threading.Tasks.Task<Services.Reservation> GetReservationByIdAsync(int reservationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddReservation", ReplyAction="http://tempuri.org/IService1/AddReservationResponse")]
        System.Threading.Tasks.Task<int> AddReservationAsync(Services.AddReservationDTO reservationDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteReservationId", ReplyAction="http://tempuri.org/IService1/DeleteReservationIdResponse")]
        System.Threading.Tasks.Task<bool> DeleteReservationIdAsync(int reservationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetReservationConfirmationPdf", ReplyAction="http://tempuri.org/IService1/GetReservationConfirmationPdfResponse")]
        System.Threading.Tasks.Task<byte[]> GetReservationConfirmationPdfAsync(int reservationId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface IService1Channel : Services.IService1, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Services.IService1>, Services.IService1
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public Service1Client() : 
                base(Service1Client.GetDefaultBinding(), Service1Client.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IService1.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), Service1Client.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Services.Flight>> GetFlightsAsync()
        {
            return base.Channel.GetFlightsAsync();
        }
        
        public System.Threading.Tasks.Task<Services.Flight> AddFlightAsync(Services.AddEditFlightDTO flight)
        {
            return base.Channel.AddFlightAsync(flight);
        }
        
        public System.Threading.Tasks.Task<bool> EditFlightAsync(Services.AddEditFlightDTO flight)
        {
            return base.Channel.EditFlightAsync(flight);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteFlightAsync(int flightId)
        {
            return base.Channel.DeleteFlightAsync(flightId);
        }
        
        public System.Threading.Tasks.Task<Services.Reservation> GetReservationByIdAsync(int reservationId)
        {
            return base.Channel.GetReservationByIdAsync(reservationId);
        }
        
        public System.Threading.Tasks.Task<int> AddReservationAsync(Services.AddReservationDTO reservationDTO)
        {
            return base.Channel.AddReservationAsync(reservationDTO);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteReservationIdAsync(int reservationId)
        {
            return base.Channel.DeleteReservationIdAsync(reservationId);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetReservationConfirmationPdfAsync(int reservationId)
        {
            return base.Channel.GetReservationConfirmationPdfAsync(reservationId);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:49363/Service1.svc/Service1");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return Service1Client.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return Service1Client.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IService1,
        }
    }
}