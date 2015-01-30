using System;

namespace Tidred.WebApp.Controllers.ApiData.Dto
{
    public class EntryParams
    {
        public long? CustomerId { get; set; }
        public long? ProjectId { get; set; }
        public string UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}