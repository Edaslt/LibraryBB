using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BB
{
    public class Workers
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RFID { get; set; }
        public int ID { get; set; }

        public Workers()
        {
        }
        public Workers(int id, string firstname, string lastName, string rfID)
        {
            ID = id;
            FirstName = firstname;
            LastName = lastName;
            RFID = rfID;
          
        }
        public override string ToString()
        {
            return $"ID:{ID} Name:{FirstName} LastName:{LastName} RFID:{RFID}";
        }


    }
}
