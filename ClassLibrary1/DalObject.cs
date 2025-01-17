﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Runtime.CompilerServices;


namespace DalApi
{
    namespace DalObject
    {

        internal sealed class DalObject : IDal
        {
            // make the access to DalObject by singelton way
            static readonly DalObject instance = new DalObject();
            internal static DalObject Instance{ get{ return instance; } }
            [MethodImpl(MethodImplOptions.Synchronized)] public List<double> PowerConsumptionBySkimmer()
            {
                List<double> dou = new List<double>();

                dou.Add( DataSource.Config.vacant);
                dou.Add(DataSource.Config.lightWeight);
                dou.Add(DataSource.Config.mediumWeight);
                dou.Add(DataSource.Config.heavyWeight);
                dou.Add(DataSource.Config.droneLoadingRate);
                return dou;


            }
            private DalObject()
            {
                DataSource.Initialize();
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void AddParcel(Parcel par)

            {
                par.runNumber++;
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public Station GetStation(int stationId)
            // Return the station with stationId
            {
               // IEnumerable<station> stations  = DataSource.stations;
               foreach(Station station in DataSource.stations)
                {
                    if (station.id == stationId)
                        return station;
                }
                throw new myExceptionDO("Exception from function GetStation", myExceptionDO.There_is_no_variable_with_this_ID);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public Drone GetDrone(int droneId)

            {
                foreach(Drone drone in DataSource.drones)
                {
                    if (drone.Id == droneId)
                        return drone;
                }
                throw new myExceptionDO("Exception from function GetDrone", myExceptionDO.There_is_no_variable_with_this_ID);
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public Customer GetCustomer(int CustomerId)

            {
                foreach (Customer customer in DataSource.customers)
                {
                    if (customer.Id == CustomerId)
                        return customer;
                }
                throw new myExceptionDO("Exception from function GetCustomer", myExceptionDO.There_is_no_variable_with_this_ID);

            }

            [MethodImpl(MethodImplOptions.Synchronized)] public Parcel GetParcel(int ParcelId)

            // Return the parcel with parcelId
            {
                for (int i = 0; i < DataSource.parcels.Count; i++)
                {
                    if (DataSource.parcels[i].Id == ParcelId)
                        return DataSource.parcels[i];

                }
                throw new myExceptionDO("Exception from function GetParcel", myExceptionDO.There_is_no_variable_with_this_ID);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void DelParcel(int ID) 
            {
                Parcel parcelToRemove = new Parcel();
            foreach(var par in DataSource.parcels)
                {
                    if (par.Id == ID)
                        parcelToRemove = par;
                }
                if (parcelToRemove.Requested != null)
                     DataSource.parcels.Remove(parcelToRemove);

            }

            [MethodImpl(MethodImplOptions.Synchronized)] public void DelStation(int ID)
            {
            foreach(var station in DataSource.stations)
                {
                    if (station.id == ID)
                        DataSource.stations.Remove(station);
                }
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public void DelCustomer(int ID) 
            {
            foreach(var customer in DataSource.customers)
                {
                    if (customer.Id == ID)
                        DataSource.customers.Remove(customer);
                }
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public void DelDrone(int ID)
            {
            foreach(var drone in DataSource.drones)
                {
                    if (drone.Id == ID)
                        DataSource.drones.Remove(drone);
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void InputTheStation(Station station)
            {
                DataSource.stations.Add(station);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void InputTheDroneCharge(DroneCharge droneCharge)
            {
                    DataSource.dronesCharge.Add(droneCharge);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void InputTheParcel(Parcel par)
            {
                DataSource.parcels.Add(par);
                AddParcel(par);//update the run-number serial
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void InputTheCustomer(Customer cust)
            {
                DataSource.customers.Add(cust);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void InputTheDrone(Drone drone)
            {             
                DataSource.drones.Add(drone);
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<DroneCharge> GetListOfDroneCharge()
            {
                List<DroneCharge> droneCharges = new List<DroneCharge>();
                
                foreach (var item in DataSource.dronesCharge)
                {
                    droneCharges.Add(item);
                }
                return droneCharges;
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<Station> GetListOfStations()
            //return all the station from DataSource.stations

            {
                if (DataSource.stations.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_stations", myExceptionDO.Dont_have_any_station_in_the_list);

                List<Station> stations = new List<Station>();
                foreach (Station station in DataSource.stations)
                    stations.Add(station);
                return stations;
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<Customer> GetListOfCustmers()
            //return all the customer from DataSource.customers
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_custmers", myExceptionDO.Dont_have_any_customer_in_the_list);

                List<Customer> customers = new List<Customer>();
                foreach (Customer customer in DataSource.customers)
                    customers.Add(customer);
                return customers;
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<DroneCharge> GetListOfDronesInCharging()
            {
                if (DataSource.dronesCharge.Count == 0)
                    throw new myExceptionDO("Don't have drones in charging", myExceptionDO.Dont_have_any_parcel_in_the_list);

                List<DroneCharge> Drones = new List<DroneCharge>();
                foreach (DroneCharge parcel in DataSource.dronesCharge)
                {
                    Drones.Add(parcel);
                }
                return Drones;
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<Parcel> GetListOfParcels()
            //print all the Parcel from DataSource.parcels

            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_Parcels", myExceptionDO.Dont_have_any_parcel_in_the_list);

                List<Parcel> parcels = new List<Parcel>();
                foreach (Parcel parcel in DataSource.parcels)
                {
                    parcels.Add(parcel);
                }
                return parcels;
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<Drone> GetListOfDrones()
            //print all the Drone from DataSource.drones
            {
                if (DataSource.drones.Count == 0)
                    throw new myExceptionDO("Exception from function Displays_list_of_drone", myExceptionDO.Dont_have_any_drone_in_the_list);

                List<Drone> drones = new List<Drone>();
                foreach (Drone drone in DataSource.drones)
                {
                    drones.Add(drone);
                }
                return drones;
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<Parcel> DisplaysParcelsDontHaveDrone()

            // Print the details of all the parcels don't have An associated skimmer (Selected_drone == 0).
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function displaysParcelsDontHaveDrone", myExceptionDO.Dont_have_any_parcel_in_the_list);

                List<Parcel> par = new();
                foreach (Parcel parcel in DataSource.parcels)
                {
                    if (parcel.Id != 0 && parcel.DroneId == 0)
                        par.Add(parcel);
                }
                return par;
            }

            [MethodImpl(MethodImplOptions.Synchronized)] public IEnumerable<Station> AvailableChargingStations()

            //Print the all stations that have DroneCharge available
            {
                if (DataSource.stations.Count == 0)
                    throw new myExceptionDO("Exception from function AvailableChargingStations", myExceptionDO.Dont_have_any_station_in_the_list);

                List<Station> stat = new();
                IEnumerator iter = DataSource.stations.GetEnumerator();
                foreach (Station station in DataSource.stations)
                {
                    if (station.ChargeSlots != 0)
                        stat.Add(station);
                }
                return stat;
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public string MinimumFromCustomer(double minDistance, Point p)

            {
                if(DataSource.customers.Count == 0)
                    throw new myExceptionDO("Exception from function MinimumFromCustomer", myExceptionDO.Dont_have_any_customer_in_the_list);

                int saveTheI = 0;// save the index with minimum destance from the point p
                foreach (Customer customer in DataSource.customers)// (int i = 1; i < IDAL.DalObject.DataSource.Config.customersIndex; i++)
                {
                    double distance = customer.location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        saveTheI++;
                        minDistance = distance;
                    }
                }
                return ("The minimum distancefrom the point is: " + minDistance +
                   "\nThe id of customer is: " + DataSource.customers[saveTheI].Id);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public string MinimumFromStation(double minDistance, Point p){
                
                  if(DataSource.customers.Count == 0) 
                    throw new myExceptionDO("Exception from function MinimumFromStation", myExceptionDO.Dont_have_any_station_in_the_list);

            int saveTheI = 0;// save the index with minimum destance from the point p
                IEnumerator iter = DataSource.stations.GetEnumerator();
                foreach (Station station in DataSource.stations)
                {
                    double distance = station.Location.distancePointToPoint(p);
                    if (minDistance > distance)
                    {
                        saveTheI++;
                        minDistance = distance;
                    }
                }
                return ("The minimum distance from the point is: " + minDistance +
                    "\nThe id of station is: " + DataSource.stations[saveTheI].id);

            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void AffiliationDroneToParcel(int parcelID, int droneID)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function AffiliationDroneToParcel", myExceptionDO.Dont_have_any_parcel_in_the_list);

                Parcel parcel = new Parcel();
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == parcelID)
                    {
                        parcel = DataSource.parcels[i];
                        parcel.DroneId = droneID;
                        DataSource.parcels[i] = parcel;
                        break;
                    }
                }

                throw new myExceptionDO("Exception from function AffiliationDroneToParcel", myExceptionDO.There_is_no_variable_with_this_ID);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void PickUp(int parcelId)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function pickUp", myExceptionDO.Dont_have_any_parcel_in_the_list);

                Parcel parcel = new Parcel();
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == parcelId)
                    {
                        parcel = DataSource.parcels[i];
                        parcel.PickedUp = DateTime.Now;
                        DataSource.parcels[i] = parcel;
                        break;
                    }
                }

                throw new myExceptionDO("Exception from function pickUp", myExceptionDO.There_is_no_variable_with_this_ID);;
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void Delivered(int deliId)
            {
                if (DataSource.parcels.Count == 0)
                    throw new myExceptionDO("Exception from function delivered", myExceptionDO.Dont_have_any_parcel_in_the_list);

                Parcel tempParcel = new Parcel();
                for (int i = 0; i < DataSource.parcels.Count(); i++)
                {
                    if (DataSource.parcels[i].Id == deliId)
                    {
                        tempParcel = DataSource.parcels[i];
                        tempParcel.Delivered = DateTime.Now;
                        DataSource.parcels[i] = tempParcel;
                        break;
                    }
                }

                throw new myExceptionDO("Exception from function delivered", myExceptionDO.There_is_no_variable_with_this_ID);

            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void SetFreeStation(int droneId)
            {
                if (DataSource.stations.Count == 0)
                    throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.Dont_have_any_station_in_the_list);
                if (DataSource.drones.Count == 0)
                    throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.Dont_have_any_drone_in_the_list);

                for (int i = 0; i < DataSource.dronesCharge.Count(); i++)
                {
                    if (DataSource.dronesCharge[i].DroneId == droneId)
                    {
                       
                        for (int j = 0; j < DataSource.stations.Count; j++)
                        { 
                            if (DataSource.stations[j].id == DataSource.dronesCharge[i].staitionId)
                            {
                                Station station = new Station();
                                station = DataSource.stations[j];
                                station.ChargeSlots++;
                                DataSource.stations[j] = station;
                            }
                            else if (j == DataSource.stations.Count() - 1)
                                throw new myExceptionDO(myExceptionDO.We_ge_to_the_end_of_list_and_dont_find_the_station);
                        }

                        DataSource.dronesCharge.RemoveAt(i); 

                        break;
                    }
                    else if (i == DataSource.drones.Count() - 1)
                           throw new myExceptionDO("Exception from function setFreeStation", myExceptionDO.We_ge_to_the_end_of_list_and_dont_find_the_drone);
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void DroneToCharge(int droneId, int stationId)
            {
                if (DataSource.stations.Count == 0)
                    throw new myExceptionDO("Exception from function droneToCharge", myExceptionDO.Dont_have_any_station_in_the_list);
                if (DataSource.drones.Count == 0)
                    throw new myExceptionDO("Exception from function droneToCharge", myExceptionDO.Dont_have_any_drone_in_the_list);

                foreach (var droneCharging in DataSource.dronesCharge) // Check if The drone already is in charge  
                {
                    if(droneCharging.DroneId == droneId)
                    {
                        throw new myExceptionDO("The drone already is in charge.");
                    }
                }

                for (int i = 0; i < DataSource.stations.Count; i++) // Find if the station exists
                {
                    if(DataSource.stations[i].id == stationId)
                    {
                        DroneCharge droneCharge = new DroneCharge();
                        droneCharge.DroneId = droneId;
                        droneCharge.staitionId = stationId;
                        DataSource.dronesCharge.Add(droneCharge);

                        Station station = new Station();
                        station = DataSource.stations[i];
                        station.ChargeSlots--;
                        DataSource.stations[i] = station;

                    }
                }

            }
            #region update methodes
            [MethodImpl(MethodImplOptions.Synchronized)] public void updateParcel(DO.Parcel parcel)
            {
                for (int i = 0; i < DataSource.parcels.Count; i++)
                {
                    if (DataSource.parcels[i].Id == parcel.Id)
                    {
                        DataSource.parcels[i] = parcel;
                    }
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void updateDrone(DO.Drone drone)
            {
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == drone.Id)
                    {
                        DataSource.drones[i] = drone;
                    }
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void updateStation(DO.Station station)
            {
                for (int i = 0; i < DataSource.stations.Count; i++)
                {
                    if (DataSource.stations[i].id == station.id)
                    {
                        DataSource.stations[i] = station;
                    }
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void updateCustomer(DO.Customer customer)
            {
                for (int i = 0; i < DataSource.customers.Count; i++)
                {
                    if (DataSource.customers[i].Id == customer.Id)
                    {
                        DataSource.customers[i] = customer;
                    }
                }
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void updateDroneToCharge(DroneCharge droneCharge)
            {
                InputTheDroneCharge(droneCharge);
            }
            [MethodImpl(MethodImplOptions.Synchronized)] public void updateRelaseDroneFromCharge(int droneId, double longi, double lati, double min)
            {
                DO.Point stationLocation = new DO.Point();
                stationLocation.latitude = lati;
                stationLocation.longitude = longi;
                for (int i = 0; i < DataSource.drones.Count; i++)
                {
                    if (DataSource.drones[i].Id == droneId)
                    {
                        DO.Drone drone = GetDrone(droneId);
                        drone.droneStatus = DO.Enum.DroneStatus.Avilble;
                        DataSource.drones[i] = drone;
                    }
                }
                for (int i = 0; i < DataSource.stations.Count; i++)
                {
                    if (DataSource.stations[i].Location.latitude == stationLocation.latitude && DataSource.stations[i].Location.longitude == stationLocation.longitude)
                    {
                        DO.Station station = DataSource.stations[i];
                        station.ChargeSlots++;
                        DataSource.stations[i] = station;
                    }
                }
                for (int i = 0; i < DataSource.dronesCharge.Count; i++)
                {
                    if (DataSource.dronesCharge[i].DroneId == droneId)
                        DataSource.dronesCharge.RemoveAt(i);

                }
            }
            #endregion
        }
    }


}
