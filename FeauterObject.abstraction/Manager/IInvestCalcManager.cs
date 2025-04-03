using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore.Abstraction.DTO;

namespace FeauterObject.abstraction.Manager
{
    public  interface IInvestCalcManager
    {


        Task<InvestmentResultDTO> Calcinvestment(InvestmentCalculationDTO investment);
    }
}
