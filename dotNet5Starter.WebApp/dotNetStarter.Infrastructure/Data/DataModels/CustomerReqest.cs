using dotNet5Starter.Webapp.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5Starter.Infrastructure.Data.DataModels
{
    public class CustomerReqest
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public DateTime Timestamp { get; set; }
        public string ReqestTitle { get; set; }
        public string RequestText { get; set; }
    }
}
