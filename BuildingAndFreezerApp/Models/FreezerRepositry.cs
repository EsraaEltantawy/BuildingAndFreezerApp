using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildingAndFreezerApp.Models
{
    public class FreezerRepositry
    {
        public IEnumerable<Trader> Trader { get; set; }
        public IEnumerable<Customer> Customer { get; set; }
        public IEnumerable<Payment> Payment { get; set; }
        public IEnumerable<BankFreezer> BankFreezer { get; set; }
    }
}