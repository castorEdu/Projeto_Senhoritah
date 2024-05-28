using Senhoritah.API.Model.Base;

namespace Senhoritah.API.Model
{
    public class BuyModel:BaseEntity
    {
        public long IdProduct { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public long IdUnit { get; set; }
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }
        public decimal Weight { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
