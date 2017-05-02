using SCCL.Domain.Abstract;
using SCCL.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCCL.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITestimonialRepository _repository;

        public HomeController(ITestimonialRepository testimonialRespository)
        {
            _repository = testimonialRespository;
        }


        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();

            homeViewModel.Testimonials = _repository.Testimonials;
            
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