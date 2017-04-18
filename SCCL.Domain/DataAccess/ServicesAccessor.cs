using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCCL.Domain.Entities;

namespace SCCL.Domain.DataAccess
{
    public class ServicesAccessor
    {
        public static List<Service> RetrieveServices()
        {
            var services = new List<Service>();
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_retrieve_services";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
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

        public static bool CreateService(Service service)
        {
            var rowsAffected = 0;

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
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return rowsAffected == 1;
        }

        public static bool UpdateService(Service oldService, Service newService)
        {
            var rowsAffected = 0;

            var conn = DbConnection.GetConnection();
            var cmdText = @"sp_update_service";

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
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return rowsAffected == 1;

        }

        public static bool DeleteService(int id)
        {
            var rowsAffected = 0;

            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_delete_service";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return rowsAffected == 1;
        }
    }
}