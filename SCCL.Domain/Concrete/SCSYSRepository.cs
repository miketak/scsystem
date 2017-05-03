using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SCCL.Domain.Abstract;
using SCCL.Domain.DataAccess;
using SCCL.Domain.Entities;

namespace SCCL.Domain.Concrete
{
    public class SCSYSRepository : ISolutionRepository, IServiceRepository, ITestimonialRepository
    {

        /// <summary>
        /// DB Context for Solutions
        /// </summary>
        public IEnumerable<Solution> Solutions
        {
            get { return SolutionsAccessor.RetrieveSolutions(); }
        }


        /// <summary>
        /// DB Context for Services
        /// </summary>
        public IEnumerable<Service> Services
        {
            get { return ServicesAccessor.RetrieveServices(); }
        }


        /// <summary>
        /// DB Context for Testimonials
        /// </summary>
        public IEnumerable<Testimonial> Testimonials
        {
            get { return TestimonialAccessor.RetrieveTestimonials(); }
        }


        public Testimonial Testimonial
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                TestimonialAccessor.UpdateTestimonial(value);
            }
        }

        /// <summary>
        /// Deletes a testimonial by id
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeteteTestimonial(int id)
        {
            return TestimonialAccessor.DeleteTestimonial(id);
        }

        /// <summary>
        /// Creates a new testimonial
        /// 
        /// </summary>
        /// <param name="testimonial"></param>
        /// <returns></returns>
        public bool CreateTestimonial(Testimonial testimonial)
        {
            return TestimonialAccessor.CreateTestimonial(testimonial);
        }
    }
}
