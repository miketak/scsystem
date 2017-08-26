using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCCL.Core.Interfaces;
using SCCL.Infrastructure;

namespace SCCL.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioRepository _repository;

        public PortfolioController(IPortfolioRepository repo)
        {
            _repository = repo;
        }

        // GET: Portfolio
        [Route("ourworks")]
        public ActionResult Index()
        {
            var portfolioIndex = _repository.PortfolioIndex;

            return View(portfolioIndex);
        }

        [Route("ourworks/{id}")]
        public ActionResult Detail(int id)
        {
            var portfolioDetail = _repository.RetrievePortfolioDetailById(id);

            portfolioDetail.PortfolioImages = _repository.RetrievePortfolioImageIdsById(id);

            return View(portfolioDetail);

        }


        public FileContentResult GetImage(int id)
        {
            var portfolioItem = _repository.RetrievePortfolioDetailById(id);

            return portfolioItem != null ? File(portfolioItem.Thumbnail, portfolioItem.ThumbnailMimeType) : null;
        }

        public ActionResult GetPortfolioImage(int id)
        {
            var portfolioImage = _repository.RetrievePortfolioImageById(id);

            return portfolioImage != null ? File(portfolioImage.Image, portfolioImage.ImageMimeType) : null;
        }
    }
}