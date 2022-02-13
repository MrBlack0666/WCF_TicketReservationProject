using Services;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using WebServiceClient.Handler;

namespace WebServiceClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Service1Client service;

        private ObservableCollection<Flight> flights;

        private Flight selectedFlight;

        public string password;

        public MainWindow()
        {
            InitializeComponent();

            var binding = new BasicHttpBinding();
            binding.MessageEncoding = WSMessageEncoding.Mtom;

            service = new Service1Client(binding, new System.ServiceModel.EndpointAddress("http://localhost:49363/Service1.svc/Service1"));
            service.Endpoint.EndpointBehaviors.Add(new InspectorGadgetBehavior());
            this.DataContext = this;
            this.selectedFlight = null;
            this.password = "";
            

            FlightDetailsGrid.Visibility = Visibility.Hidden;

            GetFlights();
        }

        private async void GetFlights()
        {
            try
            {
                var flightsArray = await service.GetFlightsAsync();
                flights = new ObservableCollection<Flight>(flightsArray);

                FlightsListBox.ItemsSource = flights;
            }
            catch (FaultException faultException)
            {
                var a = faultException.Message;
            }
        }

        private void AddFlightButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditFlightWindow addEditFlightWindow = new AddEditFlightWindow(null, service);
            addEditFlightWindow.ShowDialog();
            if (addEditFlightWindow.flight != null)
            {
                 flights.Add(addEditFlightWindow.flight);
            }
        }

        private void EditFlightButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditFlightWindow addEditFlightWindow = new AddEditFlightWindow(this.selectedFlight, service);
            addEditFlightWindow.ShowDialog();
            if (addEditFlightWindow.flight == null && addEditFlightWindow.isEditing == true)
            {
                GetFlights();
            }
        }

        private void FlightsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            this.selectedFlight = (Flight)((ListBox)sender).SelectedItem;
            if(selectedFlight != null)
            {
                FlightFromLabel.Content = selectedFlight.From_City;
                FlightToLabel.Content = selectedFlight.To_City;
                FlightArrivalDateLabel.Content = selectedFlight.FlightArrivalDateString;
                FlightDepartureDateLabel.Content = selectedFlight.FlightDepartureDateString;
                PriceLabel.Content = selectedFlight.Price + " zł";
                NumberOfAvaiableSeatsLabel.Content = selectedFlight.NumberOfAvaiableSeats;
                NumberOfSeatsLabel.Content = selectedFlight.NumberOfSeats;

                this.FlightDetailsGrid.Visibility = Visibility.Visible;
            }
            else
            {
                this.FlightDetailsGrid.Visibility = Visibility.Hidden;
            }
        }

        private async void DeleteFlightButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                password = "";
                ConfrimActionWithPassword confrimActionWithPassword = new ConfrimActionWithPassword("Czy na pewno chcesz usunąć lot?", "Usuń");
                confrimActionWithPassword.ShowDialog();
                if (confrimActionWithPassword.confirmed)
                {
                    password = confrimActionWithPassword.password;
                    await service.DeleteFlightAsync(selectedFlight.Id);
                    password = "";
                    selectedFlight = null;
                    GetFlights();
                }
            }
            catch(FaultException faultException)
            {
                password = "";
                MessageBoxResult messageBoxResult = MessageBox.Show(faultException.Reason.ToString(), faultException.Code.Name, MessageBoxButton.OK);
            }
        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            AddReservationWindow addReservationWindow = new AddReservationWindow(selectedFlight, service);
            addReservationWindow.ShowDialog();
            if (addReservationWindow != null)
            {
                GetFlights();
            }
        }

        private async void SearchReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(SearchReservationTextBox.Text))
            {
                return;
            }

            var reservation = await service.GetReservationByIdAsync(int.Parse(SearchReservationTextBox.Text));

            if(reservation != null)
            {
                DisplayReservationDetailsWindow displayReservationDetailsWindow = new DisplayReservationDetailsWindow(reservation, service);
                displayReservationDetailsWindow.ShowDialog();
                if(displayReservationDetailsWindow.reservation == null)
                {
                    GetFlights();
                }
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Nie znaleziono rezerwacji", "Błąd wyszukiwania", MessageBoxButton.OK);
            }
        }
    }
}
