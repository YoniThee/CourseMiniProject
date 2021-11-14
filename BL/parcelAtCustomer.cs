﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class parcelAtCustomer
        {
            public int uniqueID { get; set; }
            public Enum_BO.WeightCategories weight { get; set; }
            public Enum_BO.Priorities priority { get; set; }
            public Enum_BO.Situations situation { get; set; }
            public CustomerInParcel customer_In_Delivery { get; set; }
            public override string ToString()
            {
                return $"Parcel ID = {uniqueID}, weight: {Enum.GetName(typeof(Enum_BO.WeightCategories), weight)}," +
                    $" priority: {Enum.GetName(typeof(Enum_BO.Priorities), priority)}, situation: {Enum.GetName(typeof(Enum_BO.Situations),situation)}" +
                    $", customer in delivery: {customer_In_Delivery.ToString()}";
            }
        }
    }
}