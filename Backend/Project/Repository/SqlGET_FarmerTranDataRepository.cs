using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerTranDataRepository : IGET_FarmerTranDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerTranDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerTranData>> getasync(GET_FarmerTranDataDomain input)
        {
            List<GET_FarmerTranData> result = new List<GET_FarmerTranData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerTranData](@p0 , @p1 , @p2 , @p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.REFAID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_FarmerTranData
                        {
                           STATUS = reader.GetBoolean("STATUS"),
                           ADVAMT = reader.GetDouble("ADVAMT"),
                           TRAID = reader.GetString("TRAID"),
                           REFAID = reader.GetString("REFAID"),
                           CBAL = reader.GetDouble("CBAL"),
                           MAID = reader.GetString("MAID"),
                           CROPID = reader.GetString("CROPID"),
                           TRNDT = reader.GetString("TRNDT"),
                           TRNSID = reader.GetString("TRNSID"),
                            NARR = reader.GetString("NARR"),
                           TRNSR = reader.GetInt32("TRNSR"),
                           PAMT = reader.GetDouble("PAMT")

                        });
                    }
                }
            }
            return result;
        }
    }
}
