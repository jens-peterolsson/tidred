using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tidred.WebApp.Controllers.ApiData.Dto
{
    public class ProjectData
    {
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long FixedPrice { get; set; }
    }
}