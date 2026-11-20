using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRM_HolidayRepository : IGET_HRM_HolidayRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRM_HolidayRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_HRM_Holiday>> getasync(GET_HRM_HolidayDomain input)
        {
            List<GET_HRM_Holiday> list = new List<GET_HRM_Holiday> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_HRMHoliday](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.HAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_HRM_Holiday
                        {
                            HAID = reader.GetString("HAID"),
                            ADDHOLIDAY = reader.GetString("ADDHOLIDAY"),
                            SDATE = reader.GetString("SDATE"),
                            EDATE = reader.GetString("EDATE"),
                            NODAYS = reader.GetInt32("NODAYS"),
                            HASTATUS = reader.GetBoolean("HASTATUS"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_HRM_Holiday>> getkeyword(GET_HRM_HolidayDomain input)
        {
            List<GET_HRM_Holiday> list = new List<GET_HRM_Holiday>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HRMHoliday](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.HAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_HRM_Holiday
                        {
                            HAID = reader.GetString("HAID"),
                            ADDHOLIDAY = reader.GetString("ADDHOLIDAY"),
                            SDATE = reader.GetString("SDATE"),
                            EDATE = reader.GetString("EDATE"),
                            NODAYS = reader.GetInt32("NODAYS"),
                            HASTATUS = reader.GetBoolean("HASTATUS"),
                        });
                    }
                }
            }
            return list;
        }
           
    }
}
