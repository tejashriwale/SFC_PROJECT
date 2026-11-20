using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRMShiftInfoRepository : IGET_HRMShiftInfoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRMShiftInfoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_HRMShiftInfo>> getasync(GET_HRMShiftInfoDomain input)
        {
            List<GET_HRMShiftInfo> list = new List<GET_HRMShiftInfo>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_HRMShiftInfo](@p0 , @p1  ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_HRMShiftInfo
                        {
                            SHID = reader.GetString("SHID"),
                            SHNAME = reader.GetString("SHNAME"),
                            SHFROMTIME = reader.GetString("SHFROMTIME"),
                            SHTOTIME = reader.GetString("SHTOTIME"),
                            WEEKOFF = reader.GetString("WEEKOFF"),
                            SHDESCRIPTION = reader.GetString("SHDESCRIPTION"),
                            SHSTATUS = reader.GetBoolean("SHSTATUS"),
                            MORNINGBREAKFROM = reader.GetString("MORNINGBREAKFROM"),
                            MORNINGBREAKTO = reader.GetString("MORNINGBREAKTO"),
                            LUNCHFROM = reader.GetString("LUNCHFROM"),
                            LUNCHTO = reader.GetString("LUNCHTO"),
                            EVENINGBREAKFROM = reader.GetString("EVENINGBREAKFROM"),
                            EVENINGBREAKTO = reader.GetString("EVENINGBREAKTO"),
                            WEEKDAY = reader.GetString("WEEKDAY"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_HRMShiftInfo>> getkeyword(GET_HRMShiftInfoDomain input)
        {
            List<GET_HRMShiftInfo> list = new List<GET_HRMShiftInfo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HRMShiftInfo](@p0 , @p1  ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_HRMShiftInfo
                        {
                            SHID = reader.GetString("SHID"),
                            SHNAME = reader.GetString("SHNAME"),
                            SHFROMTIME = reader.GetString("SHFROMTIME"),
                            SHTOTIME = reader.GetString("SHTOTIME"),
                            WEEKOFF = reader.GetString("WEEKOFF"),
                            SHDESCRIPTION = reader.GetString("SHDESCRIPTION"),
                            SHSTATUS = reader.GetBoolean("SHSTATUS"),
                            MORNINGBREAKFROM = reader.GetString("MORNINGBREAKFROM"),
                            MORNINGBREAKTO = reader.GetString("MORNINGBREAKTO"),
                            LUNCHFROM = reader.GetString("LUNCHFROM"),
                            LUNCHTO = reader.GetString("LUNCHTO"),
                            EVENINGBREAKFROM = reader.GetString("EVENINGBREAKFROM"),
                            EVENINGBREAKTO = reader.GetString("EVENINGBREAKTO"),
                            WEEKDAY = reader.GetString("WEEKDAY"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
