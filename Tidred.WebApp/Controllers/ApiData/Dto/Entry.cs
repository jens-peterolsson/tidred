namespace Tidred.WebApp.Controllers.ApiData.Dto
{
    public class Entry
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long? ProjectId { get; set; }
        public decimal Hours { get; set; }
        public string Comment { get; set; }
        public string Day { get; set; }
        public long PriceTypeId { get; set; }
        public int? Rate { get; set; }

    }
}