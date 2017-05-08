using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;
using SCCL.Infrastructure;

namespace SCCL.Web.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialRepository _repository;


        public TestimonialController(ITestimonialRepository testimonialRepository)
        {
            _repository = testimonialRepository;
        }

        // GET: Testimonial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            Testimonial testimonial = null;

            try
            {
                testimonial = _repository.FindById(id);
            }
            catch( Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            if (testimonial == null)
                return RedirectToAction("Index", "SiteAdmin");
            
            return View("Edit", testimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Testimonial newTestimonial)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "SiteAdmin");

            try
            {
                _repository.Edit(newTestimonial);
                return RedirectToAction("Index", "SiteAdmin", new {area = ""});
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == DbError.UpdateFailed.ToString())
                    return RedirectToAction("Edit");
                if (ex.Message == DbError.ConcurrencyError.ToString())
                    return RedirectToAction("Index", "SiteAdmin");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "SiteAdmin");
        }

        public ActionResult Delete(int id)
        {
            try
            {
               _repository.Remove(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Index", "SiteAdmin", new { area = "" });
            }

            return RedirectToAction("Index", "SiteAdmin", new { area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Testimonial testimonial)
        {
            if (!ModelState.IsValid) 
                return RedirectToAction("Index", "SiteAdmin");

            try
            {
                _repository.Add(testimonial);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Index", "SiteAdmin");
            }


            return RedirectToAction("Index", "SiteAdmin");
        }

        public ActionResult Create()
        {
            return View(new Testimonial());
        }

    }
}