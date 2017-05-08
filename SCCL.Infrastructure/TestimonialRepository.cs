using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;

namespace SCCL.Infrastructure
{
    public class TestimonialRepository : ITestimonialRepository
    {

        public IEnumerable<Testimonial> Testimonials
        {
            get
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
        }

        public void Add(Testimonial testimonial)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_create_testimonial";

            using (var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure})
            {
                cmd.Parameters.AddWithValue("@AUTHOR", testimonial.Author);
                cmd.Parameters.AddWithValue("@MESSAGE", testimonial.Message);

                try
                {
                    conn.Open();
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if ( rowsAffected != 1)
                        throw new ApplicationException(DbError.CreateFailed.ToString());
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public void Remove(int testimonialId)
        {
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_delete_testimonial";

            using (var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@Id", testimonialId);

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

        public void Edit(Testimonial newTestimonial)
        {
            var oldTestimonial = Testimonials.FirstOrDefault(t => t.Id == newTestimonial.Id);

            if (oldTestimonial == null)
                throw new ApplicationException(DbError.ConcurrencyError.ToString());

            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_update_testimonial";

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

        public Testimonial FindById(int testimonialId)
        {
            return Testimonials.FirstOrDefault(x => x.Id == testimonialId);
        }
    }
}