using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;

namespace SCCL.Infrastructure
{
    public class ContactMessageRepository : IContactMessageRepository
    {

        public IEnumerable<ContactMessage> ContactMessages
        {
            get
            {
                var contactMessages = new List<ContactMessage>();
                var conn = DbConnection.GetConnection();
                const string cmdText = @"sp_ContactMessagesSelect";

                using (var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure})
                {
                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                var contactMessage = new ContactMessage
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Subject = reader.GetString(3),
                                    Message = reader.GetString(4),
                                    DatePosted = reader.GetDateTime(5)
                                };
                                contactMessages.Add(contactMessage);
                            }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                return contactMessages;
                
            }
        }

        public void Add(ContactMessage contactMessage)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_ContactMessagesInsert";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Name", contactMessage.Name);
                cmd.Parameters.AddWithValue("@Email", contactMessage.Email);
                cmd.Parameters.AddWithValue("@Subject", contactMessage.Subject);
                cmd.Parameters.AddWithValue("@MessageBody", contactMessage.Message);
                cmd.Parameters.AddWithValue("@TimePosted", DateTime.Now); 

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected != 1)
                        throw new ApplicationException(DbError.CreateFailed.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Edit(ContactMessage newContactMessage)
        {
            var oldContactMessage = ContactMessages.FirstOrDefault(x => x.Id == newContactMessage.Id);

            if (oldContactMessage == null)
                throw new ApplicationException(DbError.ConcurrencyError.ToString());

            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_ContactMessagesUpdate";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Name", newContactMessage.Name);
                cmd.Parameters.AddWithValue("@Email", newContactMessage.Email);
                cmd.Parameters.AddWithValue("@Subject", newContactMessage.Subject);
                cmd.Parameters.AddWithValue("@MessageBody", newContactMessage.Message);
                cmd.Parameters.AddWithValue("@TimePosted", newContactMessage.DatePosted);

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected != 1)
                        throw new ApplicationException(DbError.ConcurrencyError.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }   
        }

        public void Remove(int id)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_ContactMessagesDelete";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected != 1)
                        throw new ApplicationException(DbError.DeleteFailed.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public ContactMessage FindById(int id)
        {
            ContactMessage contactMessage = null;
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_ContactMessagesSelect_By_Id";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        contactMessage = new ContactMessage
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Subject = reader.GetString(3),
                            Message = reader.GetString(4),
                            DatePosted = reader.GetDateTime(5)
                        };  
                    }
                       
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return contactMessage;
        }
    }
}