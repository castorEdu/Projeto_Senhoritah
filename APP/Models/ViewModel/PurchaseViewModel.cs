namespace APP.Models.ViewModel
{
    public class PurchaseViewModel
    {
        public BillsModel Bills { get; set; } = new BillsModel();
        public IEnumerable<BillsModel> ListBills { get; set; } = Enumerable.Empty<BillsModel>();
    }
}
