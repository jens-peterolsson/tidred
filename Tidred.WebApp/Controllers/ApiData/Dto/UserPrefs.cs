using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tidred.WebApp.Controllers.ApiData.Dto
{
    public class UserPrefs
    {
        public string UserId { get; set; }
        public long CustomerId { get; set; }
        public long? ProjectId { get; set; }
        public long? PriceTypeId { get; set; }
    }
}