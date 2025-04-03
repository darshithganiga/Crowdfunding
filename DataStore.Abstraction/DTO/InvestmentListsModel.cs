using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.DTO
{
    public class InvestmentListsModel
    {
        public int InvestmentId { get; set; }

        public int CampaignId { get; set; }
        public decimal AmountInvested { get; set; }
        public string CompanyName { get; set; }
        public string ProductImage { get; set; }
        public int ShareBuyed { get; set; }
        public int EquityOwned { get; set; }
    }
}
