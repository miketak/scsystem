using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Testimonial> Testimonials { get; set; }
    }
}