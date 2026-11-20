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
                command.CommandText = "SELECT Top 20 * FROM [GET_ServiceCharge](@p0, @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SCID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT * FROM [GET_ServiceCharge](@p0, @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SCID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
