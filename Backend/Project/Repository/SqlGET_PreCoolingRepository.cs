using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PreCoolingRepository : IGET_PreCoolingRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PreCoolingRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PreCooling>> getasync(GET_PreCoolingDomain input)
        {
            List<GET_PreCooling> list = new List<GET_PreCooling>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PreCooling](@p0 , @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PCID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PreCooling
                        {

                            PCID = reader.GetString("PCID"),
                            DATE = reader.GetString("DATE"),
                            RATE = reader.GetDouble("RATE"),
                            STATUS = reader.GetString("STATUS"),
                            DAYSFROM = reader.GetString("DAYSFROM"),
                            DAYSTO = reader.GetString("DAYSTO"),
                           

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PreCooling>> getkeyword(GET_PreCoolingDomain input)
        {
            List<GET_PreCooling> list = new List<GET_PreCooling>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_PreCooling](@p0 , @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PCID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PreCooling
                        {

                            PCID = reader.GetString("PCID"),
                            DATE = reader.GetString("DATE"),
                            RATE = reader.GetDouble("RATE"),
                            STATUS = reader.GetString("STATUS"),
                            DAYSFROM = reader.GetString("DAYSFROM"),
                            DAYSTO = reader.GetString("DAYSTO"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
