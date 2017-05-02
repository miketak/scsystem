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
        private readonly ITestimonialRepository _testimonialrepository;
        private SolutionServiceViewModel solutionservices;
        private SiteAdminViewModel siteAdminViewModel;

        public SiteAdminController(ISolutionRepository solutionRepository, IServiceRepository serviceRepository,
            ITestimonialRepository testimonialRepository)
        {
            _solutionrepository = solutionRepository;
            _servicerepository = serviceRepository;
            _testimonialrepository = testimonialRepository;
        }

        // GET: SiteAdmin
        public ActionResult Index()
        {
            //solutionservices = new SolutionServiceViewModel
            //{
            //    Solutions = _solutionrepository.Solutions,
            //    Services = _servicerepository.Services
            //};

            siteAdminViewModel = new SiteAdminViewModel
            {
                Solutions = _solutionrepository.Solutions,
                Services = _servicerepository.Services,
                Testimonials = _testimonialrepository.Testimonials
            };

            return View(siteAdminViewModel);
        }


    }
}