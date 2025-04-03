using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.DTO
{
    public class HomePageDTO
    {
        public int CampaignID { get; set; }

        public string CompanyName { get; set; }
        public string ProductImage { get; set; }

        public decimal FundingGoal { get; set; }
        public decimal FundsRaised { get; set; }


        public DateTime EndDate { get; set; }

        public string Status { get; set; }



    }
}
