using Microsoft.VisualStudio.TestTools.UnitTesting;
using BB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        [TestMethod()]
        public void RetrieveLists()
        {
            // arrange 
            var res = new Repository();
            var work = new Workers();
            // act

            var workerList = res.RetrieveObjects<Workers>();
            foreach (var worker in workerList)
            {
                Console.WriteLine(worker.FirstName);
            }
            var productList = res.RetrieveObjects<Products>();
            foreach (var product in productList)
            {
                Console.WriteLine(product);
            }
            // assert  
            Assert.AreEqual(workerList[0].FirstName, "Petras");

        }

        [TestMethod()]
        public void UpdateObjectsTest()
        {
            // arrange 
            var res = new Repository();
            var work = new Workers();
            var prod = new Products();
            // act
            work.ID = 1;
            work.FirstName = "Test";
            work.LastName = "LastTest";
            work.RFID = "AS452";
            var result = res.WriteObjects<Workers>(work,Repository.SQLActions.replace);

            prod.ID = 9;
            prod.Name = "product A";
            prod.FilletSize = 12;
            prod.FilletQty = 0.1F;
            prod.LiquidQty = 0.2F;
            prod.ContainerName = "container C";
            prod.Additives = "additive";
            var result2 = res.WriteObjects<Products>(prod,Repository.SQLActions.update);

            var workplace = new Workplaces();
            workplace.GroupName = "kk";
            workplace.Occupied = 1;
            workplace.WorkerID = 5;
            workplace.PlaceID = 1;
            var result3 = res.WriteObjects<Workplaces>(workplace, Repository.SQLActions.replace);
            // assert 
            Assert.AreEqual(result, 1);
            Assert.AreEqual(result2, 1);
        }

        [TestMethod()]
        public void RetrieveObjectsByGroupTest()
        {
            // arrange 
            var res = new Repository();
            // act

            var workplaceList = res.RetrieveObjects<Workplaces>("kk");

            foreach (var place in workplaceList)
            {
                Console.WriteLine(place);
            }
            workplaceList = res.RetrieveObjects<Workplaces>("");

            foreach (var place in workplaceList)
            {
                Console.WriteLine(place);
            }
          var groupNameList =  Workplaces.GetGroupNames(workplaceList);
            foreach (var name in groupNameList)
            {
                Console.WriteLine(name);
            }
            // assert 


            Assert.AreEqual(groupNameList[0],"kk");
        }
    }
}