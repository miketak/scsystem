using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface ITestimonialRepository
    {
        /// <summary>
        /// Retrieves an IEnumerable implementation of services
        /// </summary>
        IEnumerable<Testimonial> Testimonials { get; }

        /// <summary>
        /// Adds testimonial to persistent storage
        /// </summary>
        /// <param name="testimonial"></param>
        void Add(Testimonial testimonial);


        /// <summary>
        /// Deletes testimonial from persistent storage
        /// </summary>
        /// <param name="testimonialId"></param>
        void Remove(int testimonialId);

        /// <summary>
        /// Updates testimonial in persistent storage
        /// </summary>
        /// <param name="newTestimonial"></param>
        void Edit(Testimonial newTestimonial);

        /// <summary>
        /// Finds testimonial by id
        /// </summary>
        /// <param name="testimonialId"></param>
        /// <returns></returns>
        Testimonial FindById(int testimonialId);
    }
}