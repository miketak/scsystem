using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SCCL.Core.Entities;
using SCCL.Core.Interfaces;

namespace SCCL.Infrastructure
{
    public class PortfolioRepository : IPortfolioRepository
    {
        public IEnumerable<Portfolio> PortfolioIndex
        {
            get
            {
                var portfolioIndex = new List<Portfolio>();
                var conn = DbConnection.GetConnection();
                const string cmdText = @"sp_retrieve_portfolio";

                using (var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure})
                {
                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var portfolio = new Portfolio
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    ResearchArea = reader.GetString(2),
                                    Author = reader.GetString(3),
                                    Publisher = reader.GetString(4),
                                    LinkUrl = reader.GetString(5),
                                    PortfolioType = reader.GetString(6),
                                    Summary = reader.GetString(7),
                                    Thumbnail = reader.IsDBNull(9) ? null : reader["Thumbnail"] as byte[],
                                    ThumbnailMimeType = reader.GetString(9)
                                };
                                portfolioIndex.Add(portfolio);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                return portfolioIndex;
            }
        }

        public Portfolio RetrievePortfolioDetailById(int id)
        {
            Portfolio portfolioDetail = null;
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_retrieve_portfolio_by_id";

            using (var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure})
            {
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                            portfolioDetail = new Portfolio
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                ResearchArea = reader.GetString(2),
                                Author = reader.GetString(3),
                                Publisher = reader.GetString(4),
                                LinkUrl = reader.GetString(5),
                                PortfolioType = reader.GetString(6),
                                Summary = reader.GetString(7),
                                Thumbnail = reader.IsDBNull(8) ? null : reader["Thumbnail"] as byte[],
                                ThumbnailMimeType = reader.GetString(9)
                            };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return portfolioDetail;
            }
        }

        public IEnumerable<PortfolioImage> RetrievePortfolioImagesById(int id)
        {
            var portfolioImages = new List<PortfolioImage>();
            var conn = DbConnection.GetConnection();
            const string cmdText = @"sp_portfolio_images_by_id";

            using (var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure})
            {
                cmd.Parameters.AddWithValue(@"PortfolioId", id);

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.HasRows)
                    {
                        var portfolioImage = new PortfolioImage
                        {
                            Id = reader.GetInt32(0),
                            PortfolioId = reader.GetInt32(1),
                            Image = reader.IsDBNull(2) ? null : reader["PortfolioImage"] as byte[],
                            ImageMimeType = reader.GetString(3)
                        };
                        portfolioImages.Add(portfolioImage);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            return portfolioImages;
        }
    }
}