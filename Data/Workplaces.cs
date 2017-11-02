using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB
{
    public class Workplaces
    {
        
        public int PlaceID { get; set; }
        public int WorkerID { get; set; }
        public int Occupied { get; set; }
        public string GroupName { get; set; }

        public Workplaces()
        {
        }
        public Workplaces(int placeid, int workerID, int occupied, string groupName)
        {
            PlaceID = placeid;
            WorkerID = workerID;
            Occupied = occupied;
            GroupName = groupName;

        }

        public static List<string> GetGroupNames(List<Workplaces> workplaceList)=>
        workplaceList.Select(x => x.GroupName).Distinct().ToList(); 


        public override string ToString()
        {
            return $"PlaceID:{PlaceID} WorkerID:{WorkerID} GroupName:{GroupName} Occupied:{Occupied} ";
        }
    }
}
