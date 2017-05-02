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
                // gets by id
                throw new System.NotImplementedException();
            }
            set
            {
                TestimonialAccessor.UpdateTestimonial(value);
            }
        }
    }
}
