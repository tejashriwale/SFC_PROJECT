using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_AllTransectionsDataRepository : IGET_AllTransectionsDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AllTransectionsDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AllTransectionsData>> getasync(GET_AllTransectionsDataDomain input)
        {
            List<GET_AllTransectionsData> list = new List<GET_AllTransectionsData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_AllTransectionsData](@p0 , @p1 , @p2, @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.REFAID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AllTransectionsData
                        {
                           MAID = reader.GetString("MAID"),
                           ADVAMT = reader.GetDouble("ADVAMT"),
                           REFAID = reader.GetString("REFAID"),
                           TRNSR = reader.GetInt32("TRNSR"),
                           CBAL = reader.GetDouble("CBAL"),
                           PAMT = reader.GetDouble("PAMT"),
                           TRAID = reader.GetString("TRAID"),
                           TRNTID = reader.GetString("TRNTID"),
                           STATUS = reader.GetBoolean("STATUS")

                        });
                    }
                }
            }
            return list;
        }
    }
}
