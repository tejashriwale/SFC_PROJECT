using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_ServiceChargeRepository : IGET_ServiceChargeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ServiceChargeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ServiceCharge>> getasync(GET_ServiceChargeDomain input)
        {
            List<GET_ServiceCharge> list = new List<GET_ServiceCharge>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ServiceCharge]('" + input.SCID + "', '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ServiceCharge
                        {
                            SCID	= reader.GetString("SCID"),
                            RATEAPPLICABLE	= reader.GetString("RATEAPPLICABLE"),
                            SERVICETYPE	= reader.GetString("SERVICETYPE"),
                            RATETYPE	 = reader.GetString("RATETYPE"),
                            RATE = reader.GetDouble("RATE"),
                            ISAPPROVAL = reader.GetBoolean("ISAPPROVAL"),
                            RTYPE = reader.GetString("RTYPE"),
                            STYPE = reader.GetString("STYPE"),
                            RAPPLI = reader.GetString("RAPPLI"),
                            LEJAR = reader.GetString("LEJAR"),
                            LEJARL = reader.GetString("LEJARL"),
                            DATE = reader.GetString("DATE"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_ServiceCharge>> getkeyword(GET_ServiceChargeDomain input)
        {
            List<GET_ServiceCharge> list = new List<GET_ServiceCharge>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ServiceCharge]('" + input.SCID + "', '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ServiceCharge
                        {
                            SCID = reader.GetString("SCID"),
                            RATEAPPLICABLE = reader.GetString("RATEAPPLICABLE"),
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                            RATETYPE = reader.GetString("RATETYPE"),
                            RATE = reader.GetDouble("RATE"),
                            ISAPPROVAL = reader.GetBoolean("ISAPPROVAL"),
                            RTYPE = reader.GetString("RTYPE"),
                            STYPE = reader.GetString("STYPE"),
                            RAPPLI = reader.GetString("RAPPLI"),
                            LEJAR = reader.GetString("LEJAR"),
                            LEJARL = reader.GetString("LEJARL"),
                            DATE = reader.GetString("DATE"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
