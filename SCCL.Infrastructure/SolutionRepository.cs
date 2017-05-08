using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;

namespace SCCL.Infrastructure
{
    public class SolutionRepository : ISolutionRepository
    {
        public IEnumerable<Solution> Solutions
        {
            get
            {
                var solutions = new List<Solution>();
                var conn = DbConnection.GetConnection();
                const string cmdText = @"sp_retrieve_solutions";

                using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
                {
                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                var solution = new Solution
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    ImageMimeType = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    ImageData = reader.IsDBNull(4) ? null : reader["ImageData"] as byte[]
                                };
                                solutions.Add(solution);
                            }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                return solutions;
            }
        }

        public void Add(Solution solution)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_create_solution";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Name", solution.Name);
                cmd.Parameters.AddWithValue("@Description", solution.Description);

                if (solution.ImageData == null)
                {
                    cmd.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1);
                    cmd.Parameters["@ImageData"].Value = DBNull.Value;

                    cmd.Parameters.Add("@ImageMimeType", SqlDbType.VarChar);
                    cmd.Parameters["@ImageMimeType"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageData", solution.ImageData);
                    cmd.Parameters.AddWithValue("@ImageMimeType", solution.ImageMimeType);
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

        public void Edit(Solution newSolution)
        {
            var oldSolution = Solutions.FirstOrDefault(x => x.Id == newSolution.Id);

            if (oldSolution == null)
                throw new ApplicationException(DbError.ConcurrencyError.ToString());

            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_update_solution";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", oldSolution.Id);

                cmd.Parameters.AddWithValue("@OldName", oldSolution.Name);
                cmd.Parameters.AddWithValue("@OldDescription", oldSolution.Description);

                cmd.Parameters.AddWithValue("@NewName", newSolution.Name);
                cmd.Parameters.AddWithValue("@NewDescription", newSolution.Description);

                if (newSolution.ImageData == null)
                {
                    cmd.Parameters.Add("@NewImageData", SqlDbType.VarBinary, -1);
                    cmd.Parameters["@NewImageData"].Value = DBNull.Value;

                    cmd.Parameters.Add("@NewImageMimeType", SqlDbType.VarChar);
                    cmd.Parameters["@NewImageMimeType"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NewImageData", newSolution.ImageData);
                    cmd.Parameters.AddWithValue("@NewImageMimeType", newSolution.ImageMimeType);
                }

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if ( rowsAffected != 1)
                        throw new ApplicationException(DbError.UpdateFailed.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public Solution FindById(int solutionId)
        {
            return Solutions.FirstOrDefault(s => s.Id == solutionId);
        }

        public void Remove(int solutionId)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_delete_solution";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", solutionId);

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
    }
}