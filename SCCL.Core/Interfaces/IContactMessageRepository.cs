using System.Collections.Generic;
using SCCL.Core.Entities;

namespace SCCL.Core.Interfaces
{
    public interface IContactMessageRepository
    {
        /// <summary>
        /// Retrieves an IEnumerable implementation Contact Messages
        /// </summary>
        IEnumerable<ContactMessage> ContactMessages { get; }

        /// <summary>
        /// Adds a new contact message to persistent storage
        /// </summary>
        /// <param name="contactMessage"></param>
        void Add(ContactMessage contactMessage);

        /// <summary>
        /// Updates contact message in persistent storage
        /// </summary>
        /// <param name="newContactMessage"></param>
        void Edit(ContactMessage newContactMessage);

        /// <summary>
        /// Removes contact message from persistent storage
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);

        /// <summary>
        /// Finds a contact message by id
        /// </summary>
        /// <param name="id">Contact Message Id</param>
        /// <returns></returns>
        ContactMessage FindById(int id);
        
    }
}