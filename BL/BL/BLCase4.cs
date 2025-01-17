﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace BlApi
{
    public partial class BL : IBL
    {
      

        #region Get List Of...
        // Return list of entity_BO ('entity' to list).
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToTheList> GetListOfBaseStations()
        {
            List<BO.StationToTheList> stations = GetAllStaionsBy(p => true).ToList();

            if (stations.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_base_stations'", MyExeption_BO.An_empty_list);

            return stations;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetTheListOfDrones()
        {
            return ListDroneToList;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetListOfCustomers()
        {

            List<BO.CustomerToList> customersToList = GetAllCustomersBy(p => true).ToList();

            if (customersToList.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_customers'", MyExeption_BO.An_empty_list);

            return customersToList;

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetTheListOfParcels()
        {
            List<BO.ParcelToList> customersToList = GetAllParcelsBy(p => true).ToList();

            if (customersToList.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_the_list_of_Parcels'", MyExeption_BO.An_empty_list);

            return customersToList;
        }
        #endregion

        #region Get All List By...
        // Filter functions of list with entity_DO and return list with entity_BO (after the filter).
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetAllParcelsBy(System.Predicate<DO.Parcel> filter)
        {
            List<ParcelToList> parcelsToLists = new List<ParcelToList>();

            parcelsToLists.AddRange(accessDal.GetListOfParcels() // Get IEnumerable of all parcels.
                .ToList() // Comvert from IEnumerable to list.
                .FindAll(filter) // Filter the list by the filter.
                .ConvertAll(convertParcelDoToParcelBo));// Convert parcel_do to parcel_bo

            return parcelsToLists;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToTheList> GetAllStaionsBy(System.Predicate<DO.Station> filter)
        {
            List<StationToTheList> StationsToTheList = new List<StationToTheList>();

            StationsToTheList.AddRange(accessDal.GetListOfStations() // Get IEnumerable of all stations.
                .ToList() // Comvert from IEnumerable to list.
                .FindAll(filter) // Filter the list by the 'filter'.
                .ConvertAll(convertStaionDoToStaionBo));// convert station_do to station_bo


            return StationsToTheList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetAllCustomersBy(System.Predicate<DO.Customer> filter)
        {
            List<CustomerToList> customersToList = new List<CustomerToList>();

            customersToList.AddRange(accessDal.GetListOfCustmers() // Get IEnumerable of all customers.
                     .ToList() // Comvert from IEnumerable to list.
                .FindAll(filter) // Filter the list by the 'filter'.
                .ConvertAll(convertCustomerDoToCustomerBo));// convert customer_do to customer_bo

            return customersToList;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneInCharging> GetAllDronesInCharging(System.Predicate<DO.DroneCharge> filter)
        {
            List<DroneInCharging> DronesToList = new List<DroneInCharging>();

            DronesToList.AddRange(accessDal.GetListOfDronesInCharging().ToList() // List of all drones in BO.
                .FindAll(filter).ConvertAll(convertDroneChargeDoToDroneInChargingBo)); // Filter the list by the 'filter'.

            return DronesToList;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetAllDronesBy(System.Predicate<BO.DroneToList> filter)
        {
            try
            {
                List<DroneToList> DronesToList = new List<DroneToList>();

                DronesToList.AddRange(ListDroneToList // List of all drones in BO.
                    .FindAll(filter)); // Filter the list by the 'filter'.

                return DronesToList;
            }
            catch (Exception)
            {
                throw new BO.MyExeption_BO("No_object_by_this_filter");
            }
        }

        #endregion

        #region convert
        ParcelToList convertParcelDoToParcelBo(DO.Parcel item)
        //  Convert functions from entity_DO to entity_BO.

        {
            try
            {
                ParcelToList ParcelToListBO = new ParcelToList();

                ParcelToListBO = new ParcelToList();
                ParcelToListBO.uniqueID = item.Id;
                ParcelToListBO.nameTarget = accessDal.GetCustomer(item.TargetId).name;
                ParcelToListBO.namrSender = accessDal.GetCustomer(item.SenderId).name;
                ParcelToListBO.priority = (BO.EnumBO.Priorities)(int)item.priority;
                ParcelToListBO.weight = (BO.EnumBO.WeightCategories)(int)item.weight;
                ParcelToListBO.parcelsituation = FunParcelSituation(item);

                return ParcelToListBO;

            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'convertParcelDoToParcelBo'", e);
            }


        }
        StationToTheList convertStaionDoToStaionBo(DO.Station staion)
        {
            StationToTheList stationForTheList = new StationToTheList();

            stationForTheList.uniqueID = staion.id;
            stationForTheList.name = staion.name;
            stationForTheList.availableChargingStations = staion.ChargeSlots;

            stationForTheList.unAvailableChargingStations = accessDal.GetListOfDroneCharge()
                .ToList() // Comvert from IEnumerable to list.
               .FindAll(droneCarge_DO => droneCarge_DO.staitionId == staion.id) // Return list with all the droneCarge_DO == staion.id 
                .Count(); // Return count of item in the list.


            return stationForTheList;
        }
        CustomerToList convertCustomerDoToCustomerBo(DO.Customer customer)
        {
            CustomerToList customerToList = new CustomerToList();

            customerToList.uniqueID = customer.Id;
            customerToList.name = customer.name;
            customerToList.phone = customer.phone;


            // Run on the list parcel and find the parcels the related him (the customer).
            accessDal.GetListOfParcels().ToList().ForEach(delegate (DO.Parcel parcel)
            {

                // packages sent and delivered
                if (parcel.SenderId == customerToList.uniqueID && parcel.Delivered != null)
                    customerToList.packagesSentAndDelivered++;
                // packages sent and not delivered
                if (parcel.SenderId == customerToList.uniqueID && parcel.PickedUp != null && parcel.Delivered == null)
                    customerToList.packagesSentAndNotDelivered++;
                // packages he received
                if (parcel.TargetId == customerToList.uniqueID && parcel.Delivered != null)
                    customerToList.packagesHeReceived++;
                // packages on the way to the customer
                if (parcel.TargetId == customerToList.uniqueID && parcel.PickedUp != null && parcel.Delivered == null)
                    customerToList.packagesOnTheWayToTheCustomer++;

            });

            return customerToList;
        }
        DroneInCharging convertDroneChargeDoToDroneInChargingBo(DO.DroneCharge item)
        {
            DroneInCharging droneInCharging = new DroneInCharging();
            droneInCharging.uniqueID = item.DroneId;
            droneInCharging.startCharge = item.startCharge;
            droneInCharging.batteryStatus = GetBatteryStatus(droneInCharging.uniqueID);

            return droneInCharging;
        }
        #endregion

      /// <summary>
        /// Reutrn the Situation of parcel
        /// </summary>
        BO.EnumBO.Situations FunParcelSituation(DO.Parcel p)
        {
            if (p.Delivered != null) return BO.EnumBO.Situations.provided;
            else if (p.PickedUp != null) return BO.EnumBO.Situations.collected;
            else if (p.Scheduled != null) return EnumBO.Situations.associated;
            return BO.EnumBO.Situations.created;

        }
    }
}