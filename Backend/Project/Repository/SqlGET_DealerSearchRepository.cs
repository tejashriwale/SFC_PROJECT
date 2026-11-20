using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_DealerSearchRepository : IGET_DealerSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DealerSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_DealerSearch>> getasync(GET_DealerSearchDomain input)
        {
            List<GET_DealerSearch> result = new List<GET_DealerSearch>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DealerSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DELARNAME) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_DealerSearch
                        {
                            DEAID = reader.GetString("DEAID"),
                            DELARNAME = reader.GetString("DELARNAME"),
                            RATE = reader.GetDouble("RATE"),
                            SIZE = reader.GetString("SIZE"),
                            DELARRATE = reader.GetDouble("DELARRATE"),
                            BRANDNAME = reader.GetString("BRANDNAME"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            DEALERLABEL = reader.GetString("DEALERLABEL"),
                            SUBCATEGORY = reader.GetString("SUBCATEGORY"),
                            BRANDLABEL = reader.GetString("BRANDLABEL")

                        });
                    }
                }
            }
            return result;
        }
    }
}
