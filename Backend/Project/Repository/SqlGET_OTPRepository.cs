using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_OTPRepository : IGET_OTPRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_OTPRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_OTP>> getasync(GET_OTPDomain input)
        {
            List<GET_OTP> list = new List<GET_OTP>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HRMPayroll](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKOID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_OTP
                        {
                            PKOID = reader.GetString("PKOID"),
                            MOBILE = reader.GetString("MOBILE"),
                            OTP = reader.GetInt32("OTP"),
                            VNAME = reader.GetString("VNAME"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
}
