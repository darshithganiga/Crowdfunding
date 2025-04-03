using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Abstraction.DTO
{
    public class InvestmentCalculationDTO
    {

        public int TotalShares { get; set; }

        public int SharesToBuy { get; set; }

        public decimal FundingGoal {get;set;}
        public decimal EquityOffered {get;set;}




    }
}
