using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ServiceChargesByIdRepository : IGET_ServiceChargesByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ServiceChargesByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ServiceChargesById>> getasync(GET_ServiceChargesByIdDomain input)
        {
            List<GET_ServiceChargesById> result = new List<GET_ServiceChargesById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ServiceChargesById](@p0  , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.RATEAPPLICABLE) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_ServiceChargesById
                        {
                            SCID = reader.GetString("SCID"),
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                            RATEAPPLICABLE = reader.GetString("RATEAPPLICABLE"),
                            RATETYPE = reader.GetString("RATETYPE"),
                            RATE = reader.GetDouble("RATE"),
                            ISAPPROVAL = reader.GetBoolean("ISAPPROVAL"),
                            RTYPE = reader.GetString("RTYPE"),
                            STYPE = reader.GetString("STYPE"),
                            RAPPLI = reader.GetString("RAPPLI")

                        });

                    }
                }
            }
            return result;
        }
    }
}
