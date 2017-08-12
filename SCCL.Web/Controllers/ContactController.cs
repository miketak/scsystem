using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;

namespace SCCL.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageRepository _repository;

        public ContactController(IContactMessageRepository contactRepository)
        {
            _repository = contactRepository;
        }


        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactMessage contactMessage)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Contact");

            try
            {
                _repository.Add(contactMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Index", "Contact");
            }

            return RedirectToAction("Index", "Contact");
        }
    }
}