using SCCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCCL.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Testimonial> Testimonials { get; set; }
    }
}