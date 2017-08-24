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
        private IPortfolioRepository _repository;

        public PortfolioController(IPortfolioRepository repo)
        {
            _repository = repo;
        }

        // GET: Portfolio
        public ActionResult Index()
        {
            var portfolioIndex = _repository.PortfolioIndex;

            return View(portfolioIndex);
        }

        public ActionResult Detail()
        {
            return View();
        }


        public FileContentResult GetImage(int id)
        {

            var portfolioItem = _repository.RetrievePortfolioDetailById(id);

            return portfolioItem != null ? File(portfolioItem.Thumbnail, portfolioItem.ThumbnailMimeType) : null;

        }
    }
}