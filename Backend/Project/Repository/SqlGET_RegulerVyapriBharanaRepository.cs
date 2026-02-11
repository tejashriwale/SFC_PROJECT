using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_RegulerVyapriBharanaRepository : IGET_RegulerVyapriBharanaRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RegulerVyapriBharanaRepository (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RegulerVyapriBharana>> getasync(GET_RegulerVyapriBharanaDomain input)
        {
            {
                List<GET_RegulerVyapriBharana> result = new List<GET_RegulerVyapriBharana>();
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [GET_RegulerVyapriBharana]('" + input.VDAID + "' )";
                    dbContext.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new GET_RegulerVyapriBharana
                            {
                                vdaid = reader.GetString("vdaid"),
                                VPAID = reader.GetString("VPAID"),
                                TAMT		 = reader.GetDouble("TAMT"),
                                BPAMT = reader.GetDouble("BPAMT"),
                                VAPASI	 = reader.GetDouble("VAPASI"),
                                TRNTYPE  = reader.GetString("TRNTYPE"),
                                TRNTID = reader.GetString("TRNTID"),
                                VTYPE = reader.GetString("VTYPE"),
                                
                                VNAME = reader.GetString("VNAME")
                            });
                        }
                    }
                }
                return result;
            }
        }
    }
    }