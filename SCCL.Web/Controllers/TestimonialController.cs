using SCCL.Domain.Abstract;
using SCCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCCL.Domain.DataAccess;

namespace SCCL.Web.Controllers
{
    public class TestimonialController : Controller
    {
        private ITestimonialRepository _repository;


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
                testimonial = _repository.Testimonials.FirstOrDefault(t => t.Id == id);
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
                _repository.Testimonial = newTestimonial;
                return RedirectToAction("Index", "SiteAdmin", new {area = ""});
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == DBStatus.UpdateFailed.ToString())
                    return RedirectToAction("Edit");
                if (ex.Message == DBStatus.NoLongerExists.ToString())
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
               if ( _repository.DeteteTestimonial(id) )
                   return RedirectToAction("Index", "SiteAdmin", new { area = "" });
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
                if ( _repository.CreateTestimonial( testimonial ))
                    return RedirectToAction("Index", "SiteAdmin");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return RedirectToAction("Index", "SiteAdmin");
        }

        public ActionResult Create()
        {
            return View(new Testimonial());
        }





        //public ActionResult Edit(int id)
        //{
        //    solutionservices = new SolutionServiceViewModel { Solutions = _repository.Solutions };

        //    var solution = _repository.Solutions.FirstOrDefault(p => p.Id == id);

        //    return View("Edit", solution);
        //}




    }
}