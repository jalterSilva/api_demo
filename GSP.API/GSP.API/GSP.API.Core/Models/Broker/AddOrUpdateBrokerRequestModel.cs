
namespace GSP.API.Core.Models.Broker
{
    public class AddOrUpdateBrokerRequestModel
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string BrokerName { get; set; }
        public bool Active { get; set; }

    }
}
