using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.Models
{
    public  class CampaignModel
    {
        public int CampaignID { get; set; }
        public int FundraiserID { get; set; }
        public string CompanyName { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public decimal FundingGoal { get; set; }
        public decimal FundsRaised { get; set; }
        public decimal EquityOffered { get; set; }
        public int SharesTotal { get; set; }
        public int SharesRemaining { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        


    }
}
