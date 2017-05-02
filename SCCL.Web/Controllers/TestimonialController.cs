using SCCL.Domain.Abstract;
using SCCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            if (ModelState.IsValid)
            {
                var oldTestimonial = _repository.Testimonials.FirstOrDefault(t => t.Id == newTestimonial.Id);

                try
                {
                    if (_repository.Testimonials(oldTestimonial) )//SolutionsAccessor.UpdateSolution(oldSolution, newSolution))
                        return RedirectToAction("Index", "SiteAdmin", new { area = "" });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return View(newSolution);
        }


        //// Admin Functionality

        //public ActionResult Create()
        //{
        //    return View(new Solution());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Name, Description, ImageMimeType, ImageData")] Solution solution,
        //    HttpPostedFileBase image = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (image != null)
        //        {
        //            solution.ImageMimeType = image.ContentType;
        //            solution.ImageData = new byte[image.ContentLength];
        //            image.InputStream.Read(solution.ImageData, 0, image.ContentLength);
        //        }

        //        try
        //        {
        //            if (!SolutionsAccessor.CreateSolution(solution))
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.Message);
        //        }


        //        return RedirectToAction("Index", "SiteAdmin");
        //    }

        //    return RedirectToAction("Index", "SiteAdmin");
        //}



        //public ActionResult Edit(int id)
        //{
        //    solutionservices = new SolutionServiceViewModel { Solutions = _repository.Solutions };

        //    var solution = _repository.Solutions.FirstOrDefault(p => p.Id == id);

        //    return View("Edit", solution);
        //}

        //public ActionResult Delete(int id)
        //{
        //    SolutionsAccessor.DeleteSolution(id);

        //    return RedirectToAction("Index", "SiteAdmin", new { area = "" });
        //}


    }
}