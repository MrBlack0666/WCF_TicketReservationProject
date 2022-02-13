using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WebServiceClient
{
    /// <summary>
    /// Interaction logic for AddReservationWindow.xaml
    /// </summary>
    public partial class AddReservationWindow : Window
    {
        private Flight flight;

        private Service1Client service;

        ObservableCollection<AddPersonDTO> people;

        AddPersonDTO selectedPerson;

        public AddReservationWindow(Flight flight, Service1Client service)
        {
            InitializeComponent();
            this.DataContext = this;
            this.service = service;

            people = new ObservableCollection<AddPersonDTO>();
            selectedPerson = null;
            this.flight = flight;
            ReservationToLabel.Content = "Rezerwacja na lot z " + flight.From_City + " do " + flight.To_City + ", dnia " + flight.FlightDepartureDateString;

            PeopleListBox.ItemsSource = people;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool PersonDetailsValidation()
        {
            bool temp = true;

            if(string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                temp = false;
            }

            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
            {
                temp = false;
            }

            if (!BirthDateDTP.Value.HasValue)
            {
                temp = false;
            }

            return temp;
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PersonDetailsValidation())
            {
                return;
            }

            people.Add(new AddPersonDTO()
            {
                FirstName = FirstNameTextBox.Text,
                Surname = SurnameTextBox.Text,
                BirthDate = (DateTime)BirthDateDTP.Value
            });

            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            FirstNameTextBox.Text = "";
            SurnameTextBox.Text = "";
            BirthDateDTP.Value = null;
        }

        private void DeletePersonButton_Click(object sender, RoutedEventArgs e)
        {
            people.Remove(selectedPerson);
            this.selectedPerson = null;
        }

        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedPerson = (AddPersonDTO)((ListBox)sender).SelectedItem;

            DeletePersonButton.Visibility = this.selectedPerson != null ? Visibility.Visible : Visibility.Hidden;
        }

        private async void AddReservationButton_Click(object sender, RoutedEventArgs e)
        {
            var reservationId = await service.AddReservationAsync(new AddReservationDTO()
            {
                FlightId = flight.Id,
                People = people.ToList()
            });
            if(reservationId > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Numer rezerwacji: " + reservationId, "Pomyślnie dodano rezerwację", MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Nie dodano rezerwacji! \nNajprawdopodobniej ilość osób jest zbyt duża, jak na ten lot.\n Maksymalna liczba osób: " + flight.NumberOfAvaiableSeats, "Błąd dodawania", MessageBoxButton.OK);
            }
        }
    }
}
