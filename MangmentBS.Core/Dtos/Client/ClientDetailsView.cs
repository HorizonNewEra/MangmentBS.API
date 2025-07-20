using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Client
{
    public class ClientDetailsView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public List<ClientPaymentView> ClientPayments { get; set; }
        public List<ClientFlatView> ClientFlats { get; set; }
    }
}
