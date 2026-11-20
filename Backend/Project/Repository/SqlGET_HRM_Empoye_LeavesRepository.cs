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
                command.CommandText = "SELECT Top 20  * FROM [GET_HRMEmpoyeLeaves](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ELID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT * FROM [GET_HRMEmpoyeLeaves](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ELID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
