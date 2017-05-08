using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface IServiceRepository
    {
        IEnumerable<Service> Services { get; }

        void Add(Service service);

        void Edit(Service newService);

        void Remove(int serviceId);

        Service FindById(int serviceId);

    }
}