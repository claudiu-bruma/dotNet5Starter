using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet5Starter.Webapp.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Exchange { get; set; }       
        public string StockTicker { get; set; }        
        public string Isin { get; set; }        
        public string Website { get; set; }
    }
}
