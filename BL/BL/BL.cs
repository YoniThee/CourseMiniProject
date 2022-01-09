﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using DO;
    using BO;
using DalXml;
using BlApi;

namespace BlApi
{

    public partial class BL : IBL
    {
        static readonly BL instance = new BL();
        internal static BL Instance { get { return instance; } }
        IDal accessDal = DalApi.DalFactory.GetDal("DalObject");
        //IDal accessDalXml = DalApi.DalFactory.GetDal("DalXml");
        List<DroneToList> ListDroneToList = new List<DroneToList>();
        private BL()
        {
            InitializeAndUpdateTheListsInIBL();
        }
        public void SimulatorStart(int droneId, Func<bool> f1, Action action)
        {
           new Simulator(Instance, droneId, f1, action, accessDal);
        }

        public void InitializeAndUpdateTheListsInIBL()
        // Do initialize if data sourse and update the list listDrons of IBL.
        {

           // DalApi.DalObject.DataSource.Initialize();

           //inaitilaize the lists by get the values from xml files
           // dataXml.initilaizeXml(accessDal);     

            BO.DroneToList droneToListBO;
            IEnumerable<DO.Drone> tempList = accessDal.GetListOfDrones();

            foreach (DO.Drone drone in tempList) // Update the list in listDrons of IBL
            {
                var rand = new Random();
                droneToListBO = new BO.DroneToList();

                droneToListBO.uniqueID = drone.Id;
                droneToListBO.Model = drone.Model;
                droneToListBO.Battery = 100; //rand.Next(20, 80);
                droneToListBO.weight = (BO.EnumBO.WeightCategories)drone.MaxWeight;
                droneToListBO.status = (BO.EnumBO.DroneStatus)drone.droneStatus;

                BO.Location l = new BO.Location();
                l.latitude = 31 + rand.NextDouble();
                l.longitude = 34 + (double)rand.NextDouble();
                droneToListBO.location = l;
                droneToListBO.packageDelivered = 0;

                ListDroneToList.Add(droneToListBO);

            
            }

        }
        public bool IsDigitsOnly(string str)
        {
            if (str == "") return false;

            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public void InsertOptions() //case 1
        {
            Console.WriteLine("press 0 to back ");
            Console.WriteLine("press 1 to add a new drone-staition");
            Console.WriteLine("press 2 to add a new drone");
            Console.WriteLine("press 3 to add a new customer");
            Console.WriteLine("press 4 to add a new parcel");
            Console.WriteLine("Choose one of the following:");
        }


        public void UpdateOptions() //case 2
        {
            Console.WriteLine("Choose one of the following:");
            Console.WriteLine("press 0 to back ");
            Console.WriteLine("press 1 to update drone");
            Console.WriteLine("press 2 to update station");
            Console.WriteLine("press 3 to update customer");
            Console.WriteLine("press 4 to send drone to charge at station");
            Console.WriteLine("press 5 to send drone from charge in station ");
            Console.WriteLine("press 6 to assign drone to parcel");
            Console.WriteLine("press 7 to update picked up parcel by drone");
            Console.WriteLine("press 8 to update delivered parcel by drone");
        }
        public void EntityDisplayOptions() //case 3
        {
            Console.WriteLine("press 0 to back ");
            Console.WriteLine("press 1 to Station View");
            Console.WriteLine("press 2 to drone View");
            Console.WriteLine("press 3 to Customer View");
            Console.WriteLine("press 4 to parcel View ");
        }

        public void ListViewOptions()//case 4
        {
            Console.WriteLine("press 0 to back.");
            Console.WriteLine("press 1 to displays a list of base stations.");
            Console.WriteLine("press 2 to displays a list of drones.");
            Console.WriteLine("press 3 to displays a list of customer.");
            Console.WriteLine("press 4 to Displays the list of parcels.");
            Console.WriteLine("press 5 to displays a list of packages that have not yet been assigned to the drone.");
            Console.WriteLine("press 6 to base stations with available charging stations.\n");
        }
        //***********new cases for PL*************
        public void DelParcel(int ID)
        {
            accessDal.DelParcel(ID);
        }
        public void DelDrone(int ID)
        {
            accessDal.DelDrone(ID);
        }

    }
}