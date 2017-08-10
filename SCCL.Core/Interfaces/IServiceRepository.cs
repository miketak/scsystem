using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface IServiceRepository
    {
        /// <summary>
        /// Retrieves an IEnumerable implementation Services
        /// </summary>
        IEnumerable<Service> Services { get; }

        /// <summary>
        /// Adds a new service to persistent storage
        /// </summary>
        /// <param name="service"></param>
        void Add(Service service);

        /// <summary>
        /// Updates service in persistent storage
        /// </summary>
        /// <param name="newService"></param>
        void Edit(Service newService);

        /// <summary>
        /// Removes service from persistent storage
        /// </summary>
        /// <param name="serviceId"></param>
        void Remove(int serviceId);

        /// <summary>
        /// Finds a service by url string
        /// </summary>
        /// <param name="id">Url String</param>
        /// <returns></returns>
        Service FindById(int id);

        /// <summary>
        /// Finds a service by a url string
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        Service FindByUrl(string urlString);

    }
}