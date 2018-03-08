using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Core.Models
{
    public class Phone
    {
        public long Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public decimal Cost { get; set; }

        public int StorageCapacity { get; set; }

        public override string ToString()
        {
            return $"{Manufacturer} {Model}";
        }
    }
}
