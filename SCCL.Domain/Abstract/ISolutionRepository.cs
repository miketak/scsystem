using System.Collections.Generic;
using SCCL.Domain.Entities;

namespace SCCL.Domain.Abstract
{
    public interface ISolutionRepository
    {
        IEnumerable<Solution> Solutions { get; }
    }
}
