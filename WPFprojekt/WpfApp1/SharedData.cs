﻿using EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceApp
{
    public class SharedData
    {
        public User uzytkownik { get; set; }

        private SharedData(User uzytkownik) {
            this.uzytkownik = uzytkownik;
        }

        
        private static SharedData _instance;

        
        public static SharedData GetInstance(User uzytkownik)
        {
            if (_instance == null)
            {
                _instance = new SharedData(uzytkownik);
            }
            return _instance;
        }

        public static void RemoveInstance()
        {
            _instance = null;
        }
        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
    }
}
