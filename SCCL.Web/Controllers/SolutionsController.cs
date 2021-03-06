﻿using System;
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
    public class SolutionsController : Controller
    {
        private readonly ISolutionRepository _repository;
        private SolutionServiceViewModel _solutionservices;

        public SolutionsController(ISolutionRepository solutionRepository)
        {
            _repository = solutionRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            _solutionservices = new SolutionServiceViewModel { Solutions = _repository.Solutions };
            ViewBag.NavTitle = "Solutions";

            Solution solution = new Solution();
            if ( _repository.Solutions.Any())
                solution = _repository.Solutions.ToList()[0] ?? new Solution();

            _solutionservices.Solution = solution;

            return View(_solutionservices);
        }

        public ViewResult Detail(int id)
        {
            _solutionservices = new SolutionServiceViewModel { Solutions = _repository.Solutions };
            ViewBag.NavTitle = "Solutions";

            var solution = _repository.FindById(id);
            _solutionservices.Solution = solution;

            return View("Index", _solutionservices);
        }

        /// <summary>
        /// Controller to Query Solution by custom routing
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        [Route("solutions/{urlString}")]
        public ActionResult DetailByUrlString(string urlString)
        {
            _solutionservices = new SolutionServiceViewModel {Solution = _repository.FindByUrl(urlString), Solutions = _repository.Solutions};

            ViewBag.NavTitle = "Solutions";

            if (_solutionservices.Solution == null)
                return View("Error");

            return View("Index", _solutionservices);
        }


        // Admin Functionality
        [Route("solutions/create")]
        public ActionResult Create()
        {
            return View(new Solution());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("solutions/create")]
        public ActionResult Create([Bind(Include = "Name, Description, ImageMimeType, ImageData, UrlString")] Solution solution,
            HttpPostedFileBase image = null)
        {
            if (!ModelState.IsValid) 
                return RedirectToAction("Index", "SiteAdmin");

            if (image != null)
            {
                solution.ImageMimeType = image.ContentType;
                solution.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(solution.ImageData, 0, image.ContentLength);
            }

            try
            {
                _repository.Add(solution);
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
        [Route("solutions/edit")]
        public ActionResult Edit([Bind(Include = "Id, Name, Description, ImageMimeType, ImageData, UrlString")] Solution newSolution, HttpPostedFileBase image = null)
        {
            if (!ModelState.IsValid) 
                return View(newSolution);

            if (image != null)
            {
                newSolution.ImageMimeType = image.ContentType;
                newSolution.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(newSolution.ImageData, 0, image.ContentLength);
            }

            try
            {
                _repository.Edit(newSolution);       
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Index", "SiteAdmin", new { area = "" });
            }

            return View(newSolution);
        }

        [Route("solutions/edit")]
        public ActionResult Edit(int id)
        {
            _solutionservices = new SolutionServiceViewModel { Solutions = _repository.Solutions };

            var solution = _repository.FindById(id);

            return View("Edit", solution);
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

        public FileContentResult GetImage(int id)
        {
            _solutionservices = new SolutionServiceViewModel { Solutions = _repository.Solutions };

            var solution = _repository.FindById(id);

            return solution != null ? File(solution.ImageData, solution.ImageMimeType) : null;
        }
    }
}