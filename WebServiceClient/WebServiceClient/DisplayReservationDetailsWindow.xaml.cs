﻿using Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DisplayReservationDetailsWindow.xaml
    /// </summary>
    public partial class DisplayReservationDetailsWindow : Window
    {
        public Reservation reservation;
        private Service1Client service;

        public DisplayReservationDetailsWindow(Reservation reservation, Service1Client service)
        {
            InitializeComponent();

            this.service = service;
            this.reservation = reservation;
            ReservationLabel.Content = "Lot z " + reservation.Flight.From_City + " do " + reservation.Flight.To_City + ", dnia " + reservation.Flight.FlightDepartureDateString;
            ReservationIdLabel.Content = reservation.Id;

            DisplayPeopleListBox.ItemsSource = reservation.People;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void DeleteReservationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć rezerwację?", "Potwierdzenie usunięcia rezerwacji", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var wasDeleted = await service.DeleteReservationIdAsync(reservation.Id);

                if (wasDeleted)
                {
                    reservation = null;
                    this.Close();
                }
            }
        }

        private async void ReservationConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pdf = await service.GetReservationConfirmationPdfAsync(reservation.Id);

                if (pdf != null)
                {
                    System.IO.File.WriteAllBytes("C:\\Users\\Mateusz\\Desktop\\Studia\\Stopień2\\RSI\\RSI_projekt1\\WebServiceClient\\confirmations\\confirmation.pdf", pdf);
                    MessageBoxResult messageBoxResult = MessageBox.Show("Pobrano plik pdf. Ścieżka: C:\\Users\\Mateusz\\Desktop\\Studia\\Stopień2\\RSI\\RSI_projekt1\\WebServiceClient\\confirmations\\confirmation.pdf", "Pobrano plik PDF", MessageBoxButton.OK);
                }
            }
            catch(Exception exception)
            {

            }
        }
    }
}
