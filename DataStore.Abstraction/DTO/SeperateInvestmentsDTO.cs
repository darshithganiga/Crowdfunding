using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.DTO
{
    public class SeperateInvestmentsDTO
    {
        public DateTime InvestmentDate { get; set; }
        public string TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public int AmountInvested { get; set; }
        public int ShareBuyed { get; set; }
        public int EquityOwned { get; set; }
    }
}
