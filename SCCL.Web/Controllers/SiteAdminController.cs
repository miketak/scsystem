using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCCL.Domain.Abstract;
using SCCL.Web.ViewModels;

namespace SCCL.Web.Controllers
{
    public class SiteAdminController : Controller
    {
        private readonly ISolutionRepository _solutionrepository;
        private readonly IServiceRepository _servicerepository;
        private SolutionServiceViewModel solutionservices;

        public SiteAdminController(ISolutionRepository solutionRepository, IServiceRepository serviceRepository)
        {
            _solutionrepository = solutionRepository;
            _servicerepository = serviceRepository;
        }

        // GET: SiteAdmin
        public ActionResult Index()
        {
            solutionservices = new SolutionServiceViewModel
            {
                Solutions = _solutionrepository.Solutions,
                Services = _servicerepository.Services
            };

            return View(solutionservices);
        }


    }
}