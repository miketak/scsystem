using System;
using System.Web.Mvc;
using SCCL.Core.Interfaces;
using SCCL.Web.ViewModels;

namespace SCCL.Web.Controllers
{
    public class SiteAdminController : Controller
    {
        private readonly ISolutionRepository _solutionrepository;
        private readonly IServiceRepository _servicerepository;
        private readonly ITestimonialRepository _testimonialrepository;
        private SiteAdminViewModel _siteAdminViewModel;

        public SiteAdminController(ISolutionRepository solutionRepository, IServiceRepository serviceRepository,
            ITestimonialRepository testimonialRepository)
        {
            _solutionrepository = solutionRepository;
            _servicerepository = serviceRepository;
            _testimonialrepository = testimonialRepository;
        }

        // GET: SiteAdmin
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                _siteAdminViewModel = new SiteAdminViewModel
                    {
                        Solutions = _solutionrepository.Solutions,
                        Services = _servicerepository.Services,
                        Testimonials = _testimonialrepository.Testimonials
                    };
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Model", "Catastrophic Error");
                throw;
            }

            return View(_siteAdminViewModel);
        }


    }
}