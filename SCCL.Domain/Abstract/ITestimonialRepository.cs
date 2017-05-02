using SCCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCL.Domain.Abstract
{
    public interface ITestimonialRepository
    {
        IEnumerable<Testimonial> Testimonials { get; }

        Testimonial Testimonial { get; set; }
    }
}
