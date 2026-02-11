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
                command.CommandText = "SELECT * FROM [GET_ServiceChargesById]('" + input.COMPANYID + "'  , '" + input.DEPTID + "' , '" + input.RATEAPPLICABLE + "')";
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
