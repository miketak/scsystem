using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SCCL.Domain.Abstract;
using SCCL.Domain.DataAccess;
using SCCL.Domain.Entities;

namespace SCCL.Domain.Concrete
{
    public class SCSYSRepository : ISolutionRepository, IServiceRepository
    {

        public IEnumerable<Solution> Solutions
        {
            get { return SolutionsAccessor.RetrieveSolutions(); }
        }

        public IEnumerable<Service> Services
        {
            get { return ServicesAccessor.RetrieveServices(); }
        }
    }
}
