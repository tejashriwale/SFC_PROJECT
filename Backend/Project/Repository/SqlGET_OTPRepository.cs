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
                command.CommandText = "SELECT * FROM [GET_HRMPayroll]('" + input.PKOID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
