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
        IEnumerable<Portfolio> PortfolioIndex { get; }

        /// <summary>
        /// Returns Portfolio Details
        /// (ID, ImageDetails, Summary, PUblisher, Research Area, Author)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Portfolio RetrievePortfolioDetailById(int id);


        /// <summary>
        /// Returns all images associated with a Portfolio item by Id
        /// </summary>
        /// <param name="portfolioId"></param>
        /// <returns>List of Portfolio Images</returns>
        IEnumerable<PortfolioImage> RetrievePortfolioImageIdsById(int portfolioId);

        /// <summary>
        /// Retrieves a Portfolio Image by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PortfolioImage RetrievePortfolioImageById(int id);
    }
}
