using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariLoginRepository : IGET_VyapariLoginRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariLoginRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariLogin>> getasync(GET_VyapariLoginDomain input)
        {
            List<GET_VyapariLogin> result = new List<GET_VyapariLogin>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariLogin](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLogin
                        {
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VEMAIL = reader.GetString("VEMAIL"),
                            VAADHAR = reader.GetString("VAADHAR"),
                            VBIRTHDAYDATE = reader.GetString("VBIRTHDAYDATE"),
                            UAID = reader.GetString("UAID"),
                          
                            
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_VyapariLogin>> getkeyword(GET_VyapariLoginDomain input)
        {
            List<GET_VyapariLogin> result = new List<GET_VyapariLogin>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariLogin](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLogin
                        {
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VEMAIL = reader.GetString("VEMAIL"),
                            VAADHAR = reader.GetString("VAADHAR"),
                            VBIRTHDAYDATE = reader.GetString("VBIRTHDAYDATE"),
                            UAID = reader.GetString("UAID"),


                        });
                    }
                }
            }
            return result;
        }
    }
}
