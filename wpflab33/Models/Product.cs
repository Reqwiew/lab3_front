using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpflab33.Models
{
    internal class Product
    {
        public string ProductName { get; set; }
        public int BarchSize { get; set; }
        public decimal BarchPrice { get; set; }
        public decimal TotalCost => BarchSize * BarchPrice;


    }
}
