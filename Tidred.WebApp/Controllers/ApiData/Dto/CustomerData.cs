using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tidred.WebApp.Controllers.ApiData.Dto
{
    public class CustomerData
    {
        public long CustomerId { get; set; }
        public string Name { get; set; }
        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public int CoId { get; set; }
    }
}