using System;

namespace GSP.API.Core.Models.Broker
{
    public class BrokerResponseModel
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string BrokerName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }
}
