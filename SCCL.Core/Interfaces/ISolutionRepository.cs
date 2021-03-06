﻿using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface ISolutionRepository
    {
        /// <summary>
        /// Retrieves an IEnumerable implementation Solution objects
        /// </summary>
        IEnumerable<Solution> Solutions { get; }

        /// <summary>
        /// Adds a new solution to persistent storage
        /// </summary>
        /// <param name="solution"></param>
        void Add(Solution solution);

        /// <summary>
        /// Removes solution from persistent storage
        /// </summary>
        /// <param name="solutionId"></param>
        void Remove(int solutionId);

        /// <summary>
        /// Updates service in persistent storage
        /// </summary>
        /// <param name="newSolution"></param>
        void Edit(Solution newSolution);

        /// <summary>
        /// Finds a service by service id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Solution FindById(int id);


        /// <summary>
        /// Finds a service by a url string
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        Solution FindByUrl(string urlString);
    }
}