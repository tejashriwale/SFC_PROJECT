using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HRM_Empoye_LeavesRepository : IGET_HRM_Empoye_LeavesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HRM_Empoye_LeavesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_HRM_Empoye_Leaves>> getasync(GET_HRM_Empoye_LeavesDomain input)
        {
            List<GET_HRM_Empoye_Leaves> list = new List<GET_HRM_Empoye_Leaves> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_HRMEmpoyeLeaves]('" + input.ELID + "' , '"+input.KEYWORD+"' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_HRM_Empoye_Leaves
                        {
                            ELID = reader.GetString("ELID"),
                            ELDAYS = reader.GetBoolean("ELDAYS"),
                            ELLEAVEDATE = reader.GetString("ELLEAVEDATE"),
                            ELLEAVETYPE = reader.GetString("ELLEAVETYPE"),
                            ELDAYTYPE = reader.GetString("ELDAYTYPE"),
                            ELREASON = reader.GetString("ELREASON"),
                            ELSDT = reader.GetString("ELSDT"),
                            ELEDT = reader.GetString("ELEDT"),
                            ELTDAYS = reader.GetInt32("ELTDAYS"),
                            ELMLEAVETYPE = reader.GetString("ELMLEAVETYPE"),
                            ELMREASON = reader.GetString("ELMREASON"),
                            LeaveStatus = reader.GetString("LeaveStatus"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_HRM_Empoye_Leaves>> getkeyword(GET_HRM_Empoye_LeavesDomain input)
        {
            List<GET_HRM_Empoye_Leaves> list = new List<GET_HRM_Empoye_Leaves>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HRMEmpoyeLeaves]('" + input.ELID + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_HRM_Empoye_Leaves
                        {
                            ELID = reader.GetString("ELID"),
                            ELDAYS = reader.GetBoolean("ELDAYS"),
                            ELLEAVEDATE = reader.GetString("ELLEAVEDATE"),
                            ELLEAVETYPE = reader.GetString("ELLEAVETYPE"),
                            ELDAYTYPE = reader.GetString("ELDAYTYPE"),
                            ELREASON = reader.GetString("ELREASON"),
                            ELSDT = reader.GetString("ELSDT"),
                            ELEDT = reader.GetString("ELEDT"),
                            ELTDAYS = reader.GetInt32("ELTDAYS"),
                            ELMLEAVETYPE = reader.GetString("ELMLEAVETYPE"),
                            ELMREASON = reader.GetString("ELMREASON"),
                            LeaveStatus = reader.GetString("LeaveStatus"),

                        });
                    }
                }
            }
            return list;
        }
            
    }
}
