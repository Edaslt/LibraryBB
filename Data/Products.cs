using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BB
{
    public class Products
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int FilletSize { get; set; }
        public float FilletQty { get; set; }
        public float LiquidQty { get; set; }
        public string ContainerName { get; set; }
        public string Additives { get; set; }

        public Products()
        {
        }
        public Products(int id, string name, int filletSize, float filletQty
            , float liquidQty, string containerName, string additives)
        {
            ID = id;
            Name = name;
            FilletSize = filletSize;
            FilletQty = filletQty;
            LiquidQty = liquidQty;
            ContainerName = containerName;
            Additives = additives;
        }
        public override string ToString()
        {
        return $"ID:{ID} Name:{Name} FilletSize:{FilletSize} FilletQty:{FilletQty:F2} LiquidQty:{LiquidQty:F2} ContainerName:{ContainerName} Additives:{Additives}";
        }

    }
}
