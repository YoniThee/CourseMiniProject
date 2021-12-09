﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DO
    {
        public struct Drone
        {//all the properties
            public int Id { get; set; }
            public string Model { get; set; }
            public Enum.WeightCategories MaxWeight { get; set; }

            // public DroneCharge stationOfCharge {get;set;}

            public Enum.DroneStatus droneStatus { get; set; }
            public override string ToString()
            {
                return $"Drone ID: {Id}, model: {Model}, maxWeight: {MaxWeight}";
            }            
        }        

    }

