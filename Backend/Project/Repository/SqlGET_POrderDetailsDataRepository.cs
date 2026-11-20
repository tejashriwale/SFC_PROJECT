using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_POrderDetailsDataRepository : IGET_POrderDetailsDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_POrderDetailsDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_POrderDetailsData>> getasync(GET_POrderDetailsDataDomain input)
        {
            List<GET_POrderDetailsData> list = new List<GET_POrderDetailsData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_POrderDetailsData](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.POAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_POrderDetailsData
                        {
                            POAID = reader.GetString("POAID"),
                            PODPRODUCT = reader.GetString("PODPRODUCT"),
                            PODUOM = reader.GetString("PODUOM"),
                            PODQUANTITY = reader.GetString("PODQUANTITY"),
                            PODRATE = reader.GetDouble("PODRATE"),
                            PODTAMT = reader.GetDouble("PODTAMT"),
                            PODIGST = reader.GetDouble("PODIGST"),
                            PODSGST = reader.GetDouble("PODSGST"),
                            PODCGST = reader.GetDouble("PODCGST"),
                            PODTOTAL = reader.GetDouble("PODTOTAL"),
                            PODAID = reader.GetString("PODAID"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
