using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_LeaveTypeRepository : IGET_LeaveTypeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_LeaveTypeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_LeaveType>> getasync(GET_LeaveTypeDomain input)
        {
            List<GET_LeaveType> list = new List<GET_LeaveType>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_LeaveType](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.LTID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_LeaveType
                        {
                            LTID = reader.GetString("LTID"),
                            LNAME = reader.GetString("LNAME"),
                            LLEAVEQUOTA = reader.GetString("LLEAVEQUOTA"),
                            LSTATUS = reader.GetBoolean("LSTATUS"),
                            LCREATEDDATE = reader.GetString("LCREATEDDATE"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_LeaveType>> getkeyword(GET_LeaveTypeDomain input)
        {
            List<GET_LeaveType> list = new List<GET_LeaveType>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_LeaveType](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.LTID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_LeaveType
                        {
                            LTID = reader.GetString("LTID"),
                            LNAME = reader.GetString("LNAME"),
                            LLEAVEQUOTA = reader.GetString("LLEAVEQUOTA"),
                            LSTATUS = reader.GetBoolean("LSTATUS"),
                            LCREATEDDATE = reader.GetString("LCREATEDDATE"),

                        });
                    }
                }
            }
            return list;
        }
          
    }
}
