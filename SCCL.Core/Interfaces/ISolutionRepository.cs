using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface ISolutionRepository
    {
        IEnumerable<Solution> Solutions { get; }

        void Add(Solution solution);

        void Remove(int solutionId);

        void Edit(Solution newSolution);

        Solution FindById(int solutionId);
    }
}