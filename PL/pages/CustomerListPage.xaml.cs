﻿using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.pages
{
    /// <summary>
    /// Interaction logic for CustomerListPage.xaml
    /// </summary>
    public partial class CustomerListPage : Page
    {

        BlApi.IBL bl;
        List<CustomerToList> customersToTheLists;
        BackgroundWorker worker;
        CustomerToList customerToListChoose;



        // When true allows the 'filters' function to be activated, otherwise there is no access.
        //We usually use this when initializing or resetting the TextBox.
        bool TurnOnFunctionFilters;


        public CustomerListPage(BlApi.IBL bL1)
        {
            bl = bL1;
            customersToTheLists = new List<CustomerToList>();
            customersToTheLists.AddRange(bl.GetListOfCustomers());


            TurnOnFunctionFilters = false;
            InitializeComponent();
            TurnOnFunctionFilters = true;
            openOptions.Visibility = Visibility.Hidden;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();

            CustomersListView.ItemsSource = customersToTheLists;
        }

        /// <summary>
        /// update the viewListCustomers in real time.
        /// When the drone run in simultor we can see the change.
        /// </summary>
        void updateTheViewListCustomersInRealTime()
        {

            EnableFiltersWithConditions();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Action theUpdateView = updateTheViewListCustomersInRealTime;

                // Dispatcher to main-thread to update the window drone.
                CustomersListView.Dispatcher.BeginInvoke(theUpdateView);
                Thread.Sleep(200);
            }


        }


        private void CustomersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CustomersListView.SelectedItem != null)
                customerToListChoose = CustomersListView.SelectedItem as BO.CustomerToList;
            
            openOptions.Visibility = Visibility.Visible;

       //     CollectionViewSource.GetDefaultView(CustomersListView.ItemsSource).Refresh();

        }
        private void CancelButtonX(object sender, RoutedEventArgs e)
        {
        }
        private void AddingNewCustomer(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl).Show();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        private void ClearFilter(object sender, RoutedEventArgs e)
        {
            CustomersListView.ItemsSource = bl.GetListOfCustomers();
            HideAndReseteAllTextBox();
        }

        private void SearchSADButton_Click(object sender, RoutedEventArgs e)
        {
            /// SAD = packages Sent And Delivered

            if (FilterSADTextBox.Visibility == Visibility.Hidden)
                FilterSADTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterSADTextBox.Text = "Search";
                FilterSADTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SearchSANDButton_Click(object sender, RoutedEventArgs e)
        {
            /// SAND = packages Sent  And Not Delivered

            if (FilterSANDTextBox.Visibility == Visibility.Hidden)
                FilterSANDTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterSANDTextBox.Text = "Search";
                FilterSANDTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SearchReceivedButton_Click(object sender, RoutedEventArgs e)
        {
            /// Received = packages He (customer) Received.

            if (FilterReceiveTextBox.Visibility == Visibility.Hidden)
                FilterReceiveTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterReceiveTextBox.Text = "Search";
                FilterReceiveTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SearchOTWButton_Click(object sender, RoutedEventArgs e)
        {
            // OTW = packages On The Way To The Customer

            if (FilterOTWTextBox.Visibility == Visibility.Hidden)
                FilterOTWTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterOTWTextBox.Text = "Search";
                FilterOTWTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SearchIDButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterIDTextBox.Visibility == Visibility.Hidden)
                FilterIDTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterIDTextBox.Text = "Search";
                FilterIDTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void SearchNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterNameTextBox.Visibility == Visibility.Hidden)
                FilterNameTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterNameTextBox.Text = "Search";
                FilterNameTextBox.Visibility = Visibility.Hidden;
            }
        }
        private void SearchPhoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterPhoneTextBox.Visibility == Visibility.Hidden)
                FilterPhoneTextBox.Visibility = Visibility.Visible;
            else
            {
                FilterPhoneTextBox.Text = "Search";
                FilterPhoneTextBox.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// If TurnOnFunctionFilters = true search with the filters
        /// else don't do nathing.
        /// </summary>
        void EnableFiltersWithConditions()
        {
            if (TurnOnFunctionFilters)
                Filters();
        }
        private void AnyFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableFiltersWithConditions();
        }
        bool isNumber(string s)
        {
            if (s.Length == 0) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] >= (int)'0' && (int)s[i] <= (int)'9')
                    continue;

                return false;
            }

            return true;
        }
        private void HideAndReseteAllTextBox()
        {
            TurnOnFunctionFilters = false;

            FilterIDTextBox.Text = "Search";
            FilterIDTextBox.Visibility = Visibility.Hidden;

            FilterNameTextBox.Text = "Search";
            FilterNameTextBox.Visibility = Visibility.Hidden;

            FilterPhoneTextBox.Text = "Search";
            FilterPhoneTextBox.Visibility = Visibility.Hidden;

            FilterSADTextBox.Text = "Search";
            FilterSADTextBox.Visibility = Visibility.Hidden;

            FilterSANDTextBox.Text = "Search";
            FilterSANDTextBox.Visibility = Visibility.Hidden;

            FilterReceiveTextBox.Text = "Search";
            FilterReceiveTextBox.Visibility = Visibility.Hidden;

            FilterOTWTextBox.Text = "Search";
            FilterOTWTextBox.Visibility = Visibility.Hidden;

            TurnOnFunctionFilters = true;
        }

        private void Filters()
        // Search by all filter togther.
        {

            try
            {
                CustomersListView.ItemsSource = null;
                customersToTheLists.Clear();
                customersToTheLists.AddRange(bl.GetListOfCustomers());

                if (isNumber(FilterIDTextBox.Text)) // Filter ID
                {
                    string id = FilterIDTextBox.Text;
                    customersToTheLists = customersToTheLists.FindAll
                        (s => s.uniqueID.ToString().Contains(id));
                }
                if (isNumber(FilterNameTextBox.Text)) // Filter name
                {
                    string name = FilterNameTextBox.Text;
                    customersToTheLists = customersToTheLists.FindAll
                        (s => s.name.Contains(name));
                }
                if (isNumber(FilterPhoneTextBox.Text)) // Filter phone
                {
                    string phone = FilterPhoneTextBox.Text;
                    customersToTheLists = customersToTheLists.FindAll
                        (s => s.phone.ToString().Contains(phone));
                }
                if (isNumber(FilterSADTextBox.Text)) // Filter Sent And Delivered"
                {
                    string SentAndDelivered = FilterSADTextBox.Text;
                    customersToTheLists = customersToTheLists.FindAll
                        (s => s.packagesSentAndDelivered.ToString().Contains(SentAndDelivered));
                }
                if (isNumber(FilterSANDTextBox.Text)) // Filter Sent And Not Delivered
                {
                    string SentAndNotDelivered = FilterSANDTextBox.Text;
                    customersToTheLists = customersToTheLists.FindAll
                        (s => s.packagesSentAndNotDelivered.ToString().Contains(SentAndNotDelivered));
                }
                if (isNumber(FilterReceiveTextBox.Text)) // Filter Receive
                {
                    string Receive = FilterReceiveTextBox.Text;
                    customersToTheLists = customersToTheLists.FindAll
                        (s => s.packagesHeReceived.ToString().Contains(Receive));
                }
                if (isNumber(FilterOTWTextBox.Text)) // Filter On The Way
                {
                    string OnTheWay = FilterOTWTextBox.Text;
                    customersToTheLists = customersToTheLists.FindAll
                        (s => s.packagesOnTheWayToTheCustomer.ToString().Contains(OnTheWay));
                }

                CustomersListView.ItemsSource = customersToTheLists;
            }
            catch (Exception)
            {

            }
        }

        private void OpenCustomerView_Click(object sender, RoutedEventArgs e)
        {
            BO.CustomerToList customer = customerToListChoose;
            new CustomerWindow(bl, customer).Show();
            openOptions.Visibility = Visibility.Hidden;
            CollectionViewSource.GetDefaultView(CustomersListView.ItemsSource).Refresh();

        }

        private void OpenNewParcelView_Click(object sender, RoutedEventArgs e)
        {
            BO.CustomerToList customer = customerToListChoose;
            ParcelWindow openNewOrder = new ParcelWindow(bl);
            openNewOrder.txtSender.Text = customer.uniqueID.ToString();
            openNewOrder.txtSender.IsEnabled = false;
            openNewOrder.Show();
            openOptions.Visibility = Visibility.Hidden;
            CollectionViewSource.GetDefaultView(CustomersListView.ItemsSource).Refresh();

        }

        private void OpenCustomrsOrderView_Click(object sender, RoutedEventArgs e)
        {
            BO.CustomerToList customer = customerToListChoose;
            ParclListWindow openMyOrder = new ParclListWindow(bl);
            openMyOrder.txtFilter.Text = customer.name;
            openMyOrder.txtFilter.IsEnabled = false;
            openMyOrder.filterCombo.SelectedIndex = 1;
            openMyOrder.filterCombo.IsEnabled = false;
            openMyOrder.Show();
            openOptions.Visibility = Visibility.Hidden;
            CollectionViewSource.GetDefaultView(CustomersListView.ItemsSource).Refresh();

        }

        private void MyShipmentView_Click(object sender, RoutedEventArgs e)
        {
            BO.CustomerToList customer = customerToListChoose;
            ParclListWindow openMyOrder = new ParclListWindow(bl);
            openMyOrder.txtFilter.Text = customer.name;
            openMyOrder.txtFilter.IsEnabled = false;
            openMyOrder.filterCombo.SelectedIndex = 2;
            openMyOrder.filterCombo.IsEnabled = false;
            openMyOrder.Show();
            openOptions.Visibility = Visibility.Hidden;
            CollectionViewSource.GetDefaultView(CustomersListView.ItemsSource).Refresh();

        }

        private void CancelOpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            CustomersListView.SelectedItem = null;
            openOptions.Visibility = Visibility.Hidden;
        }
    }
}
