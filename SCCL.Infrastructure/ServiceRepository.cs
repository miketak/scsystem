using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;

namespace SCCL.Infrastructure
{
    public class ServiceRepository : IServiceRepository
    {
        public IEnumerable<Service> Services
        {
            get
            {
                var services = new List<Service>();
                var conn = DbConnection.GetConnection();
                const string cmdText = @"sp_retrieve_services";

                using (var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure})
                {
                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                var service = new Service
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    ImageMimeType = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    ImageData = reader.IsDBNull(4) ? null : reader["ImageData"] as byte[]
                                };
                                services.Add(service);
                            }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                return services;
            }
        }

        public void Add(Service service)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_create_service";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Name", service.Name);
                cmd.Parameters.AddWithValue("@Description", service.Description);

                if (service.ImageData == null)
                {
                    cmd.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1);
                    cmd.Parameters["@ImageData"].Value = DBNull.Value;

                    cmd.Parameters.Add("@ImageMimeType", SqlDbType.VarChar);
                    cmd.Parameters["@ImageMimeType"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageData", service.ImageData);
                    cmd.Parameters.AddWithValue("@ImageMimeType", service.ImageMimeType);
                }

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected != 1)
                        throw new ApplicationException(DbError.UpdateFailed.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public void Edit(Service newService)
        {
            var oldService = Services.FirstOrDefault(x => x.Id == newService.Id);

            if (oldService == null)
                throw new ApplicationException(DbError.ConcurrencyError.ToString());

            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_update_service";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", oldService.Id);

                cmd.Parameters.AddWithValue("@OldName", oldService.Name);
                cmd.Parameters.AddWithValue("@OldDescription", oldService.Description);

                cmd.Parameters.AddWithValue("@NewName", newService.Name);
                cmd.Parameters.AddWithValue("@NewDescription", newService.Description);

                if (newService.ImageData == null)
                {
                    cmd.Parameters.Add("@NewImageData", SqlDbType.VarBinary, -1);
                    cmd.Parameters["@NewImageData"].Value = DBNull.Value;

                    cmd.Parameters.Add("@NewImageMimeType", SqlDbType.VarChar);
                    cmd.Parameters["@NewImageMimeType"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NewImageData", newService.ImageData);
                    cmd.Parameters.AddWithValue("@NewImageMimeType", newService.ImageMimeType);
                }

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected != 1)
                        throw new ApplicationException(DbError.UpdateFailed.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Service FindById(int serviceId)
        {
            return Services.FirstOrDefault(x => x.Id == serviceId);
        }

        public void Remove(int serviceId)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_delete_service";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", serviceId);

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if ( rowsAffected != 1 )
                        throw new ApplicationException(DbError.DeleteFailed.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}