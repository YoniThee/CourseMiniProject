﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace DO
    {
        public interface IDal
        {
            
            public List<double> PowerConsumptionBySkimmer();
            public void AddParcel(Parcel par);

            public Station GetStation(int stationId);
            // Return the station with stationId

            public Drone GetDrone(int droneId);

            public Customer GetCustomer(int CustomerId);
            public IEnumerable<DroneCharge> GetListOfDroneCharge();
            public Parcel GetParcel(int ParcelId);
            // Return the parcel with parcelId

            public void InputTheStation(Station station);
            public void InputTheDroneCharge(DroneCharge droneCharge);
            public void InputTheParcel(Parcel par);

            public void InputTheCustomer(Customer cust);

            public void InputTheDrone(Drone dro);



        public IEnumerable<Station> GetListOfStations();

            public IEnumerable<Customer> GetListOfCustmers();

            public IEnumerable<Parcel> GetListOfParcels();
            //Copy all the Parcel from DataSource.parcels[] to new_array_parcels.
            public IEnumerable<Drone> GetListOfDrones();
        //Copy all the Drone from DataSource.drones[] to new_array_drones.
        public IEnumerable<DroneCharge> GetListOfDronesInCharging();
        //Copy all the Drone from DataSource.drones[] to new_array_drones.
        public IEnumerable<Parcel> DisplaysParcelsDontHaveDrone();
            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            public IEnumerable<Station> AvailableChargingStations();
            public void AffiliationDroneToParcel(int parcelID, int droneID);
            public void PickUp(int PickId);

            public void Delivered(int deliId);
            public void SetFreeStation(int droneId);
            public void DroneToCharge(int droneId, int stationId);



            //Print the all stations that have DroneCharge available
            public string MinimumFromCustomer(double minDistance, Point p);

            public string MinimumFromStation(double minDistance, Point p);
        public void DelParcel(int ID);
        public void DelDrone(int ID);

        public void updateParcel(DO.Parcel parcel);
        public void updateDrone(DO.Drone drone);
        public void updateStation(DO.Station station);
        public void updateCustomer(DO.Customer customer);
        public void updateDroneToCharge(DO.DroneCharge droneCharge);
        public void updateRelaseDroneFromCharge(int droneId, double longi, double lati, double min);


        }

}

