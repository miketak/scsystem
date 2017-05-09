using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;
using SCCL.Web.ViewModels;

namespace SCCL.Web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _repository;
        private SolutionServiceViewModel _solutionServices;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _repository = serviceRepository;
        }

        // GET: Services
        public ActionResult Index()
        {
            var solutionservices = new SolutionServiceViewModel { Services = _repository.Services };
            ViewBag.NavTitle = "Services";

            Service service = new Service();
            if (_repository.Services.Any())
            {
                service = _repository.Services.ToList()[0] ?? new Service();
            }
                
      
            solutionservices.Service = service;

            return View("../Solutions/Index", solutionservices);
        }

        public ActionResult Detail(int id)
        {
            var solutionservices = new SolutionServiceViewModel { Services = _repository.Services };
            ViewBag.NavTitle = "Services";

            var service = _repository.FindById(id);
            solutionservices.Service = service;

            return View("../Solutions/Index", solutionservices);
        }

        // Admin Functionality

        [Authorize]
        public ActionResult Create()
        {
            return View(new Service());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description")] Service service, HttpPostedFileBase image = null)
        {
            if (!ModelState.IsValid) 
                return RedirectToAction("Index", "SiteAdmin");

            if (image != null)
            {
                service.ImageMimeType = image.ContentType;
                service.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(service.ImageData, 0, image.ContentLength);
            }

            try
            {
                _repository.Add(service);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            return RedirectToAction("Index", "SiteAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service newService, HttpPostedFileBase image = null) 
        {
            if (!ModelState.IsValid) 
                return View(newService);

            if (image != null)
            {
                newService.ImageMimeType = image.ContentType;
                newService.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(newService.ImageData, 0, image.ContentLength);
            }

            try
            {
                _repository.Edit(newService);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return View(newService);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            _solutionServices = new SolutionServiceViewModel { Services = _repository.Services };

            var service = _repository.FindById(id);

            return View("Edit", service);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                _repository.Remove(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Model", "Delete Unsuccessful");
                return RedirectToAction("Index", "SiteAdmin", new { area = "" });
            }

            return RedirectToAction("Index", "SiteAdmin", new {area = ""});
        }

        public FileContentResult GetImage(int id)
        {
            _solutionServices = new SolutionServiceViewModel { Services = _repository.Services };

            Service service = _solutionServices.Services.FirstOrDefault(s => s.Id == id);

            return service != null ? File(service.ImageData, service.ImageMimeType) : null;
        }
    }
}