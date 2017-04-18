using System.Collections.Generic;
using SCCL.Domain.Entities;

namespace SCCL.Domain.Abstract
{
    public interface IServiceRepository
    {
        IEnumerable<Service> Services { get; }
    }
}
