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
                command.CommandText = "SELECT Top 20 * FROM [GET_HRMHoliday]('" + input.HAID + "' , '"+input.KEYWORD+"' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "SELECT * FROM [GET_HRMHoliday]('" + input.HAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
