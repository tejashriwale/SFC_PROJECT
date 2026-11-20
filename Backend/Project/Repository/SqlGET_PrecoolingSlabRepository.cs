using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PrecoolingSlabRepository : IGET_PrecoolingSlabRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PrecoolingSlabRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PrecoolingSlab>> getasync(GET_PrecoolingSlabDomain input)
        {
            List<GET_PrecoolingSlab> list = new List<GET_PrecoolingSlab>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PrecoolingSlab](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.StartDate) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.EndDate) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.CSID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.OUTWARDKG) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PrecoolingSlab
                        {
                            CSID = reader.GetString("CSID"),
                            StartDate = reader.GetString("StartDate"),
                            DaysFrom = reader.GetInt32("DaysFrom"),
                            DaysTo = reader.GetInt32("DaysTo"),
                            SlabStartDate = reader.GetString("SlabStartDate"),
                            SlabEndDate = reader.GetString("SlabEndDate"),
                            Rate = reader.GetDouble("Rate"),
                            WEIGHTINKG = reader.GetDouble("WEIGHTINKG"),
                            DaysInSlab = reader.GetInt32("DaysInSlab"),
                            Total = reader.GetDouble("Total"),
                            OUTWARDKG = reader.GetDouble("OUTWARDKG"),
                            REMAININGS = reader.GetDouble("REMAININGS"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
