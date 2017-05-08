using SCCL.Web.ViewModels;
using System.Web.Mvc;
using SCCL.Core.Interfaces;

namespace SCCL.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestimonialRepository _repository;

        public HomeController(ITestimonialRepository testimonialRespository)
        {
            _repository = testimonialRespository;
        }


        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel {Testimonials = _repository.Testimonials};


            return View(homeViewModel);
        }

        public ViewResult AboutUs()
        {
            return View("AboutUs");
        }

        public ViewResult ContactUs()
        {
            return View("ContactUs");
        }
    }
}