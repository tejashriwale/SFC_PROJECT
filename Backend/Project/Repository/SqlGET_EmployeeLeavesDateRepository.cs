using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_EmployeeLeavesDateRepository : IGET_EmployeeLeavesDateRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_EmployeeLeavesDateRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_EmployeeLeavesDate>> getasync(GET_EmployeeLeavesDateDomain input)
        {
            List<GET_EmployeeLeavesDate> list = new List<GET_EmployeeLeavesDate> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_EmployeeLeavesDate]('" + input.ELID + "' , '"+input.KEYWORD+"' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_EmployeeLeavesDate
                        {
                            ELDAYS = reader.GetBoolean("ELDAYS"),
                            ELTDAYS = reader.GetInt32("ELTDAYS"),
                            ELDAYTYPE = reader.GetString("ELDAYTYPE"),
                            ELSDT = reader.GetString("ELSDT"),
                            ELEDT = reader.GetString("ELEDT"),
                            LeaveType = reader.GetString("LeaveType"),
                            ELSTATUS = reader.GetString("ELSTATUS"),
                            ELID = reader.GetString("ELID"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_EmployeeLeavesDate>> getkeyword(GET_EmployeeLeavesDateDomain input)
        {
            List<GET_EmployeeLeavesDate> list = new List<GET_EmployeeLeavesDate>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_EmployeeLeavesDate]('" + input.ELID + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_EmployeeLeavesDate
                        {
                            ELDAYS = reader.GetBoolean("ELDAYS"),
                            ELTDAYS = reader.GetInt32("ELTDAYS"),
                            ELDAYTYPE = reader.GetString("ELDAYTYPE"),
                            ELSDT = reader.GetString("ELSDT"),
                            ELEDT = reader.GetString("ELEDT"),
                            LeaveType = reader.GetString("LeaveType"),
                            ELID = reader.GetString("ELID"),
                        });
                    }
                }
            }
            return list;
        }
            
    }
}
