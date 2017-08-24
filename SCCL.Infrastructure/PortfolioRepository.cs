using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;

namespace SCCL.Infrastructure
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private List<Portfolio> _portfolio = new List<Portfolio>
        {
            new Portfolio
            {
                Id = 1,
                Title =
                    "ECONOMIC ASSESSMENT OF UTILIZING DIESEL DERIVED FROM USED LUBRICATING OIL FOR POWER GENERATION IN GHANA",
                ResearchArea = "Renewable Energy",
                Author = "Richard Padi",
                PortfolioType = "Publication",
                Publisher = "IJIRD, Volume 6, Issue 7, July 2017",
                LinkUrl = "http://www.ijird.com/index.php/ijird/article/view/117195",
                Summary = "There is potential to recycle used oil (UO), waste lubricating oils from automobiles or machineries to produce diesel fuel (DF) that can contribute to energy supply in Ghana. However, very little is known about the economics of recycling UO to DF (Rec-UO-to-DF) and the DF use thereof for power generation (DF-power) in Ghana. This study assessed the economics of Rec-UO-to-DF and DF-power in Ghana under 2016 economic conditions, through developing process and economic models based on literature. The economics were assessed relative to prevailing diesel price ($0.79/l) and grid power prices ($0.08-0.24/kWh- residential and $0.24-0.41/kWh- commercial). Sensitivity analysis was undertaken to establish impacts of UO pricing on DF prices, and DF pricing on power prices. Maximum Expected Selling Price of- UO (MESP-UO) and -DF (MESP-DF) required for economic viability of the Rec-UO-to-DF and DF-power respectively were determined. The results showed a 12000 l UO/day recycling facility could produce ~8420 l DF/day, which could generate 1250 kW power via a diesel generator. UO price was found influential on DF price- 10% increase in UO price yields 3.2% increase in DF price. The MESP-UO was found at $0.21/l. Regarding DF-power, 10% increase in DF price results in 7.5% increase in power price, suggesting high reliance of process economics on the DF price. Instabilities in fuel prices therefore implies high uncertainties regarding economic viability. At prevailing DF price ($0.79/l), power price for profitability was found at $0.31/kWh (within commercial range), implying DF-power is economical for only commercial use. Extension of DF-power to residential users would require 31% reduction of prevailing DF price, which could be feasible if Rec-UO-to-DF process acquires UO at $0.05/l (~24% of MESP-UO). Enforcing environmental regulations on UO management could help lower UO prices, which will promote its recycling to DF for DF power, and thus contribute to sustainable power supply."
            }
        };


        
        public List<Portfolio> PortfolioIndex()
        {
            return _portfolio;
        }

        public Portfolio RetrievePortfolioDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
