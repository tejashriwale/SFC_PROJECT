using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SOrderDetailsRepository : IGET_SOrderDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_SOrderDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SOrderDetails>> getasync(GET_SOrderDetailsDomain input)
        {
           List<GET_SOrderDetails> list = new List<GET_SOrderDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SOrderDetails] (@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SODAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SOrderDetails
                        {
                            SOAID = reader.GetString("SOAID"),
                            SODAID = reader.GetString("SODAID"),
                            SODPRODUCT = reader.GetString("SODPRODUCT"),
                            SODUOM = reader.GetString("SODUOM"),
                            SODQUANTITY = reader.GetString("SODQUANTITY"),
                            SODRATE = reader.GetDouble("SODRATE"),
                            SODTAMT = reader.GetDouble("SODTAMT"),
                            SODIGST = reader.GetDouble("SODIGST"),
                            SODSGST = reader.GetDouble("SODSGST"),
                            SODCGST = reader.GetDouble("SODCGST"),
                            SODTOTAL = reader.GetDouble("SODTOTAL"),
                            SODCESS = reader.GetDouble("SODCESS"),
                            HSNCODE = reader.GetInt32("HSNCODE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
