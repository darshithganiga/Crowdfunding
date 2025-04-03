using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.DTO
{
        public class PaymentRequest
        {
            public int UserId { get; set; }
            public int CampaignId { get; set; }
            public decimal AmountInvested { get; set; }
            public DateTime InvestmentDate { get; set; }
            public int ShareBuyed { get; set; }
            public decimal EquityOwned { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
