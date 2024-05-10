namespace APP.Models.ViewModel
{
    public class ClientViewModel
    {
        public ClientModel ClientModel { get; set; } = new ClientModel();
        public IEnumerable<ClientModel> Clients { get; set; } = Enumerable.Empty<ClientModel>();
    }
}
