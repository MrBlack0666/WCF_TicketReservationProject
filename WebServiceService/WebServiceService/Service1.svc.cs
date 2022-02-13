using IronPdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using WebServiceService.PasswordAttributes;

namespace WebServiceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private readonly string FLIGHT_FILE = HostingEnvironment.MapPath("~\\bin\\data\\Flights.txt");
        private readonly string RESERVATIONS_FILE = HostingEnvironment.MapPath("~\\bin\\data\\Reservations.txt");
        private readonly string PEOPLE_FILE = HostingEnvironment.MapPath("~\\bin\\data\\People.txt");
        private readonly string SEQUENCES_FILE = HostingEnvironment.MapPath("~\\bin\\data\\Sequences.txt");

        private readonly string CONFIRMATION_DIR = HostingEnvironment.MapPath("~\\bin\\confirmatons");


        public Service1()
        {
        }

        public List<Flight> GetFlights()
        {
            return GetFlightsFromFile();
            //using (StreamWriter outputFile = new StreamWriter("C:\\Users\\Mateusz\\Desktop\\Studia\\Stopień2\\RSI\\RSI_projekt1\\WebServiceService\\WebServiceService\\bin\\data\\Flights.txt"))
            //{
            //    outputFile.Write("This is a sentence.");
            //}
        }

        [PasswordAttribute("mateusz")]
        public Flight AddFlight(AddEditFlightDTO flightDTO)
        {
            if (!CheckAddEditFlightDTO(flightDTO))
            {
                return null;
            }

            int[] sequences = new int[3];
            using (StreamReader sr = File.OpenText(SEQUENCES_FILE))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    var seqString = s.Split(';');

                    if (seqString.Length >= 3)
                    {
                        sequences[0] = int.Parse(seqString[0]);
                        sequences[1] = int.Parse(seqString[1]);
                        sequences[2] = int.Parse(seqString[2]);
                    }
                }
            }

            using (StreamWriter sw = File.AppendText(FLIGHT_FILE))
            {
                var id = sequences[0];
                var dDate = flightDTO.FlightDepartureDate.ToString();
                var aDate = flightDTO.FlightArrivalDate.ToString();

                sequences[0]++;

                sw.Write(Environment.NewLine + id + ";" + flightDTO.To_City + ";" + flightDTO.From_City + ";" + dDate + ";" + aDate + ";" + flightDTO.Price + ";" + flightDTO.NumberOfSeats + ";" + flightDTO.NumberOfSeats);
            }

            using (StreamWriter outputFile = new StreamWriter(SEQUENCES_FILE))
            {
                outputFile.Write(sequences[0] + ";" + sequences[1] + ";" + sequences[2]);
            }

            return new Flight()
            {
                Id = sequences[0] - 1,
                To_City = flightDTO.To_City,
                From_City = flightDTO.From_City,
                FlightDepartureDate = flightDTO.FlightDepartureDate,
                FlightArrivalDate = flightDTO.FlightArrivalDate,
                Price = flightDTO.Price,
                NumberOfSeats = flightDTO.NumberOfSeats,
                NumberOfAvaiableSeats = flightDTO.NumberOfSeats
            };
        }

        [PasswordAttribute("mateusz")]
        public bool EditFlight(AddEditFlightDTO flightDTO)
        {
            if (!CheckAddEditFlightDTO(flightDTO))
            {
                return false;
            }

            var flights = GetFlightsFromFile();

            foreach (var flight in flights)
            {
                if (flight.Id == flightDTO.Id)
                {
                    if (flight.NumberOfSeats - flight.NumberOfAvaiableSeats <= flightDTO.NumberOfSeats)
                    {
                        flight.FlightArrivalDate = flightDTO.FlightArrivalDate;
                        flight.FlightDepartureDate = flightDTO.FlightDepartureDate;
                        flight.From_City = flightDTO.From_City;
                        flight.To_City = flightDTO.To_City;
                        flight.Price = flightDTO.Price;
                        flight.NumberOfAvaiableSeats = flightDTO.NumberOfSeats - (flight.NumberOfSeats - flight.NumberOfAvaiableSeats);
                        flight.NumberOfSeats = flightDTO.NumberOfSeats;

                        break;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            UpdateFlightsFile(flights);

            return true;
        }

        [PasswordAttribute("mateusz")]
        public bool DeleteFlight(int flightId)
        {
            var flights = GetFlightsFromFile();
            Flight deletingFlight = null;

            foreach (var flight in flights)
            {
                if (flight.Id == flightId)
                {
                    deletingFlight = flight;
                    break;
                }
            }

            if (deletingFlight == null)
            {
                return false;
            }

            flights.Remove(deletingFlight);

            UpdateFlightsFile(flights);

            var reservations = GetReservationsFromFile();

            var reservationsToDelete = reservations.Where(r => r.FlightId == flightId).ToList();

            var reservationsToDeleteId = reservationsToDelete.Select(r => r.Id).ToList();

            foreach (var reservation in reservationsToDelete)
            {
                reservations.Remove(reservation);
            }

            UpdateReservationsFile(reservations);

            var people = GetPeopleFromFile();
            var peopleToDelete = people.Where(p => reservationsToDeleteId.Contains(p.ReservationId)).ToList();

            foreach (var person in peopleToDelete)
            {
                people.Remove(person);
            }

            UpdatePeopleFile(people);

            return true;
        }

        public Reservation GetReservationById(int reservationId)
        {
            var reservations = GetReservationsFromFile();

            var reservation = reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
            {
                return null;
            }

            var people = GetPeopleFromFile();
            reservation.People = people.Where(p => p.ReservationId == reservationId).ToList();

            var flights = GetFlightsFromFile();
            reservation.Flight = flights.FirstOrDefault(f => f.Id == reservation.FlightId);

            if (reservation.Flight == null)
            {
                return null;
            }

            return reservation;
        }

        public int AddReservation(AddReservationDTO reservationDTO)
        {
            var flights = GetFlightsFromFile();

            var flight = flights.FirstOrDefault(f => f.Id == reservationDTO.FlightId);

            if (flight == null || flight.NumberOfAvaiableSeats - reservationDTO.People.Count < 0 || !CheckAddReservationDTO(reservationDTO))
            {
                return -1;
            }

            int[] sequences = new int[3];
            using (StreamReader sr = File.OpenText(SEQUENCES_FILE))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    var seqString = s.Split(';');

                    if (seqString.Length >= 3)
                    {
                        sequences[0] = int.Parse(seqString[0]);
                        sequences[1] = int.Parse(seqString[1]);
                        sequences[2] = int.Parse(seqString[2]);
                    }
                }
            }

            var reservations = GetReservationsFromFile();
            var people = GetPeopleFromFile();

            var reservation = new Reservation()
            {
                Id = sequences[1],
                FlightId = reservationDTO.FlightId
            };

            reservations.Add(reservation);

            var peopleInReservation = new List<Person>();

            foreach (var person in reservationDTO.People)
            {
                peopleInReservation.Add(new Person()
                {
                    Id = sequences[2]++,
                    FirstName = person.FirstName,
                    Surname = person.Surname,
                    BirthDate = person.BirthDate,
                    ReservationId = sequences[1]
                });
            }

            people.AddRange(peopleInReservation);

            sequences[1]++;
            flight.NumberOfAvaiableSeats -= reservationDTO.People.Count;

            UpdateFlightsFile(flights);
            UpdateReservationsFile(reservations);
            UpdatePeopleFile(people);

            using (StreamWriter outputFile = new StreamWriter(SEQUENCES_FILE))
            {
                outputFile.Write(sequences[0] + ";" + sequences[1] + ";" + sequences[2]);
            }


            return reservation.Id;
        }

        public bool DeleteReservationId(int reservationId)
        {
            var reservations = GetReservationsFromFile();
            var flights = GetFlightsFromFile();
            var people = GetPeopleFromFile();

            var reservationToDelete = reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservationToDelete == null)
            {
                return false;
            }

            var flightToUpdate = flights.FirstOrDefault(f => f.Id == reservationToDelete.FlightId);
            var peopleToDelete = people.Where(p => p.ReservationId == reservationId).ToList();

            flightToUpdate.NumberOfAvaiableSeats += peopleToDelete.Count;
            reservations.Remove(reservationToDelete);

            foreach (var person in peopleToDelete)
            {
                people.Remove(person);
            }

            UpdateFlightsFile(flights);
            UpdateReservationsFile(reservations);
            UpdatePeopleFile(people);

            return true;
        }

        public byte[] GetReservationConfirmationPdf(int reservationId)
        {
            var reservation = GetReservationsFromFile().FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
            {
                return null;
            }

            if (File.Exists(CONFIRMATION_DIR + "\\confirmation" + reservation.Id + ".pdf"))
            {
                return File.ReadAllBytes(CONFIRMATION_DIR + "\\confirmation" + reservation.Id + ".pdf");
            }

            var flight = GetFlightsFromFile().FirstOrDefault(f => f.Id == reservation.FlightId);

            if (flight == null)
            {
                return null;
            }

            var people = GetPeopleFromFile().Where(p => p.ReservationId == reservationId).ToList();

            HtmlToPdf confirmation = new HtmlToPdf();
            confirmation.RenderHtmlAsPdf(CreateConfirmationString(reservation, flight, people)).SaveAs(CONFIRMATION_DIR + "\\confirmation" + reservation.Id + ".pdf");
            var fileStream = File.ReadAllBytes(CONFIRMATION_DIR + "\\confirmation" + reservation.Id + ".pdf");

            return fileStream;
        }

        public List<CompositeType> GetBananas()
        {
            var a = new List<CompositeType>
            {
                new CompositeType() { BoolValue = true, StringValue = "xd" },
                new CompositeType() { BoolValue = false, StringValue = "xdxd" }
            };

            return a;
        }

        private List<Flight> GetFlightsFromFile()
        {
            var flights = new List<Flight>();

            if (!File.Exists(FLIGHT_FILE))
            {
                using (FileStream fs = File.Create(FLIGHT_FILE))
                {
                    Byte[] info =
                        new UTF8Encoding(true).GetBytes("Id;To_City;From_City;FlightDepartureDate;FlightArrivalDate;Price;NumberOfSeats;NumberOfAvaiableSeats");

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            using (StreamReader sr = File.OpenText(FLIGHT_FILE))
            {
                string s = "";
                int i = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        i = 1;
                        continue;
                    }
                    var columns = s.Split(';');

                    var flight = CreateFlight(columns);

                    if (flight != null)
                    {
                        flights.Add(flight);
                    }
                }
            }

            return flights;
        }

        private List<Reservation> GetReservationsFromFile()
        {
            var reservations = new List<Reservation>();

            if (!File.Exists(RESERVATIONS_FILE))
            {
                using (FileStream fs = File.Create(RESERVATIONS_FILE))
                {
                    Byte[] info =
                        new UTF8Encoding(true).GetBytes("Id;FlightId");

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            using (StreamReader sr = File.OpenText(RESERVATIONS_FILE))
            {
                string s = "";
                int i = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        i = 1;
                        continue;
                    }
                    var columns = s.Split(';');

                    if(columns.Length == 2)
                    {
                        var isIdValid = int.TryParse(columns[0], out var id);
                        var isFlightIdValid = int.TryParse(columns[1], out var flightId);

                        if(isIdValid && isFlightIdValid)
                        {
                            reservations.Add(new Reservation()
                            {
                                Id = id,
                                FlightId = flightId
                            });
                        }
                    }
                }

            }

            return reservations;
        }

        private List<Person> GetPeopleFromFile()
        {
            var people = new List<Person>();

            if (!File.Exists(PEOPLE_FILE))
            {
                using (FileStream fs = File.Create(PEOPLE_FILE))
                {
                    Byte[] info =
                        new UTF8Encoding(true).GetBytes("Id;FirstName;Surname;BirthDate;ReservationId");

                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            using (StreamReader sr = File.OpenText(PEOPLE_FILE))
            {
                string s = "";
                int i = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        i = 1;
                        continue;
                    }
                    var columns = s.Split(';');

                    var person = CreatePerson(columns);

                    if(person != null)
                    {
                        people.Add(person);
                    }
                }

            }

            return people;
        }

        private Person CreatePerson(string[] columns)
        {
            var isIdValid = int.TryParse(columns[0], out var id);
            var isBirthDateValid = DateTime.TryParse(columns[3], out var birthDate);
            var isReservationIdValid = int.TryParse(columns[4], out var reservationId);

            if (!isIdValid || !isBirthDateValid || !isReservationIdValid || string.IsNullOrWhiteSpace(columns[1]) || string.IsNullOrWhiteSpace(columns[2]))
            {
                return null;
            }

            return new Person()
            {
                Id = id,
                FirstName = columns[1],
                Surname = columns[2],
                BirthDate = birthDate,
                ReservationId = reservationId
            };
                
        }

        private Flight CreateFlight(string[] columns)
        {
            if (columns.Length != 8)
            {
                return null;
            }

            var isIdValid = int.TryParse(columns[0], out var id);
            var toCity = columns[1];
            var fromCity = columns[2];
            var isFlightDepartureDateValid = DateTime.TryParse(columns[3], out var flightDepartureDate);
            var isFlightArrivalDateValid = DateTime.TryParse(columns[4], out var flightArrivalDate);
            var isPriceValid = decimal.TryParse(columns[5], out var price);
            var isNumberOfSeatsValid = int.TryParse(columns[6], out var numberOfSeats);
            var isNumberOfAvaiableSeatsValid = int.TryParse(columns[7], out var numberOfAvaiableSeats);

            if (!isIdValid || !isFlightDepartureDateValid || !isFlightDepartureDateValid || !isPriceValid || !isNumberOfSeatsValid || !isNumberOfAvaiableSeatsValid)
            {
                return null;
            }

            if (id <= 0 || string.IsNullOrWhiteSpace(toCity) && string.IsNullOrWhiteSpace(fromCity))
            {
                return null;
            }

            return new Flight()
            {
                Id = id,
                To_City = toCity,
                From_City = fromCity,
                FlightDepartureDate = flightDepartureDate,
                FlightArrivalDate = flightArrivalDate,
                Price = price,
                NumberOfSeats = numberOfSeats,
                NumberOfAvaiableSeats = numberOfAvaiableSeats
            };
        }

        private bool CheckAddEditFlightDTO(AddEditFlightDTO flightDTO)
        {
            if (string.IsNullOrWhiteSpace(flightDTO.To_City) || string.IsNullOrWhiteSpace(flightDTO.To_City) || flightDTO.Price <= 0 || flightDTO.NumberOfSeats <= 0 || flightDTO.FlightArrivalDate <= flightDTO.FlightDepartureDate)
            {
                return false;
            }

            return true;
        }

        private void UpdateFlightsFile(List<Flight> flights)
        {
            using (StreamWriter sw = new StreamWriter(FLIGHT_FILE))
            {
                sw.Write("Id;To_City;From_City;FlightDepartureDate;FlightArrivalDate;Price;NumberOfSeats;NumberOfAvaiableSeats");

                foreach (var flight in flights)
                {
                    var dDate = flight.FlightDepartureDate.ToString();
                    var aDate = flight.FlightArrivalDate.ToString();

                    sw.Write(Environment.NewLine + flight.Id + ";" + flight.To_City + ";" + flight.From_City + ";" + dDate + ";" + aDate + ";" + flight.Price + ";" + flight.NumberOfSeats + ";" + flight.NumberOfAvaiableSeats);
                }
            }
        }

        private void UpdateReservationsFile(List<Reservation> reservations)
        {
            using (StreamWriter sw = new StreamWriter(RESERVATIONS_FILE))
            {
                sw.Write("Id;FlightId");

                foreach (var reservation in reservations)
                {
                    sw.Write(Environment.NewLine + reservation.Id + ";" + reservation.FlightId);
                }
            }
        }

        private void UpdatePeopleFile(List<Person> people)
        {
            using (StreamWriter sw = new StreamWriter(PEOPLE_FILE))
            {
                sw.Write("Id;FirstName;Surname;BirthDate;ReservationId");

                foreach (var person in people)
                {
                    var bDate = person.BirthDate.ToString();

                    sw.Write(Environment.NewLine + person.Id + ";" + person.FirstName + ";" + person.Surname + ";" + bDate + ";" + person.ReservationId);
                }
            }
        }

        private bool CheckAddReservationDTO(AddReservationDTO reservationDTO)
        {
            foreach(var person in reservationDTO.People)
            {
                if(string.IsNullOrWhiteSpace(person.FirstName) || string.IsNullOrWhiteSpace(person.Surname) || person.BirthDate > DateTime.Now)
                {
                    return false;
                }
            }

            return true;
        }

        private string CreateConfirmationString(Reservation reservation, Flight flight, List<Person> people)
        {
            string confirmation = "<div style=\"margin-left: 30px;\"><h1>Potwierdzenie rezerwacji</h1>" +
                "<div style=\"margin-top: 50px;\">" +
                    "<p style=\"font-size: 20px;\">Nr rezerwacji: " + reservation.Id + "</p>" +
                    "<p style=\"font-size: 20px;\">Lot z: " + flight.From_City + "</p>" +
                    "<p style=\"font-size: 20px;\">Lot do: " + flight.To_City + "</p>" +
                    "<p style=\"font-size: 20px;\">Data odlotu: " + GetStringDate(flight.FlightDepartureDate) + "</p>" +
                    "<p style=\"font-size: 20px;\">Data przylotu: " + GetStringDate(flight.FlightArrivalDate) + "</p>" +
                "</div>" +
                "<div style=\"margin-top: 30px;\">" +
                "<p style=\"font-size: 20px;\">Osoby:</p>";

            foreach (var person in people)
            {
                confirmation += string.Format("<p style=\"font-size: 18px;\">{0} {1}    urodzony: {2}</p>", person.FirstName, person.Surname, GetStringDate(person.BirthDate));
            }

            confirmation += "</div><div style=\"margin-top: 50px;\"><p style=\"font-size: 20px;\">Cena: " + flight.Price * people.Count + "</p></div></div>";

            return confirmation;
        }

        private string GetStringDate(DateTime date)
        {
            return (date.ToShortDateString() + " " + date.ToShortTimeString()).Replace('-', '/');
        }
    }
}
