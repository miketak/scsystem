﻿using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Web.ViewModels
{
    public class SiteAdminViewModel
    {
        public IEnumerable<Service> Services { get; set; }

        public IEnumerable<Solution> Solutions { get; set; }

        public IEnumerable<Testimonial> Testimonials { get; set; }

        public Service Service { get; set; }

        public Solution Solution { get; set; }

        public string ReturnUrl { get; set; }

        public Testimonial Testimonial { get; set; }
    }
}