using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperExampleAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Validity { get; set; }
        public string Lot { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
