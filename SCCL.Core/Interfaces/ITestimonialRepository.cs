using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface ITestimonialRepository
    {
        IEnumerable<Testimonial> Testimonials { get; }

        void Add(Testimonial testimonial);

        void Remove(int testimonialId);

        void Edit(Testimonial newTestimonial);

        Testimonial FindById(int testimonialId);
    }
}