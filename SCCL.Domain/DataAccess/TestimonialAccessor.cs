using SCCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCL.Domain.DataAccess
{
    public enum DBStatus
    {
        UpdateFailed,
        UpdateSuccess
    };

    public class TestimonialAccessor
    {
        /// <summary>
        /// Retrieves Testimonials from persistent storage
        /// 
        /// </summary>
        /// <returns>List of Testimonials</returns>
        public static List<Testimonial> RetrieveTestimonials()
        {
            var testimonials = new List<Testimonial>();
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_retrieve_testimonials";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            var testimonial = new Testimonial
                            {
                                Id = reader.GetInt32(0),
                                Message = reader.GetString(1),
                                Author = reader.GetString(2)
                            };
                            testimonials.Add(testimonial);
                        }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return testimonials;
        }


        /// <summary>
        /// Updates Testimonial
        /// 
        /// </summary>
        /// <param name="newTestimonial">New Testimonial to Update</param>
        internal static void UpdateTestimonial(Testimonial newTestimonial)
        {
            var oldTestimonial = RetrieveTestimonials().FirstOrDefault( t => t.Id == newTestimonial.Id);

            var rowsAffected = 0;

            var conn = DbConnection.GetConnection();
            var cmdText = @"sp_update_testimonial";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", newTestimonial.Id);

                cmd.Parameters.AddWithValue("@newAuthor", newTestimonial.Author);
                cmd.Parameters.AddWithValue("@newMessage", newTestimonial.Message);

                cmd.Parameters.AddWithValue("@oldAuthor", oldTestimonial.Author);
                cmd.Parameters.AddWithValue("@oldMessage", oldTestimonial.Message);

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

            if (rowsAffected != 1)
                throw new ApplicationException( DBStatus.UpdateFailed.ToString() );
        }
    }
}
