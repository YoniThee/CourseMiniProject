﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    { 
            public class MyExeption_BO : Exception
        {
           public static Exception There_is_no_variable_with_this_ID = new Exception( "There is no variable with this ID.");
            public static Exception An_empty_list = new Exception("An empty list.");
            public static Exception Only_numbers_should_be_type_to = new Exception("Only numbers should be type to.");
            public static Exception Get_wrong_string_for_geting_access_to_BL = new Exception("get wrong string for geting access to BL");
            public static Exception No_object_by_this_filter = new Exception("No object by this filter");
          
            public MyExeption_BO(Exception e) : base(e.ToString()) { }
            public MyExeption_BO(string s, Exception e) : base(s , e) { }
            public MyExeption_BO(string s) : base(s ) { }

        }

    }

