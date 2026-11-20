using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AUCTIONSHEDdataRepository : IGET_AUCTIONSHEDdataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AUCTIONSHEDdataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AUCTIONSHEDdata>> getasync(GET_AUCTIONSHEDdataDomain input)
        {
            List<GET_AUCTIONSHEDdata> data = new List<GET_AUCTIONSHEDdata>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AUCTIONSHEDdata](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.APKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        data.Add(new GET_AUCTIONSHEDdata
                        {
                            APKID = reader.GetString("APKID"),
                            DATE = reader.GetString("DATE"),
                            MAIDD = reader.GetString("MAIDD"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            FNAMEE = reader.GetString("FNAMEE"),
                            HAMALI = reader.GetString("HAMALI"),
                            TOKENN = reader.GetString("TOKENN"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            SEVA = reader.GetString("SEVA"),
                            TOLAI = reader.GetString("TOLAI"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            VILLAGEE = reader.GetString("VILLAGEE"),
                            
                            

                        });
                    }
                }
            }
            return data;
        }
    }
}
