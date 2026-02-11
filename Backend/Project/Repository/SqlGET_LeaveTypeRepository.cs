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
                command.CommandText = "SELECT Top 20 * FROM [GET_LeaveType]('" + input.LTID + "' , '"+input.KEYWORD+"' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "SELECT * FROM [GET_LeaveType]('" + input.LTID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
