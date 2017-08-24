using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface IPortfolioRepository
    {
        /// <summary>
        /// Returns Full Porfolio Index
        /// (ID, Thumbnail, Title, Research Area)
        /// </summary>
        /// <returns></returns>
        List<Portfolio> PortfolioIndex();

        /// <summary>
        /// Returns Portfolio Details
        /// (ID, ImageDetails, Summary, PUblisher, Research Area, Author)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Portfolio RetrievePortfolioDetails(int id);


    }
}
