using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariLimitRepository : IGET_VyapariLimitRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariLimitRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariLimit>> getasync(GET_VyapariLimitDomain input)
        {
            List<GET_VyapariLimit> result = new List<GET_VyapariLimit>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_VyapariLimit](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VLEID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLimit
                        {
                            VLEID = reader.GetString("VLEID"),
                            SHILLAKRAKKAM = reader.GetString("SHILLAKRAKKAM"),
                            ADHICHIMARYADA = reader.GetString("ADHICHIMARYADA"),
                            RAKKAMVADHAVANE = reader.GetString("RAKKAMVADHAVANE"),
                            VAYPARI = reader.GetString("VAYPARI"),
                            STATUS = reader.GetBoolean("STATUS"),
                            VNAME = reader.GetString("VNAME"),
                            VAPNAME = reader.GetString("VAPNAME"),
                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_VyapariLimit>> getkeyword(GET_VyapariLimitDomain input)
        {
            List<GET_VyapariLimit> result = new List<GET_VyapariLimit>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariLimit](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VLEID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariLimit
                        {
                            VLEID = reader.GetString("VLEID"),
                            SHILLAKRAKKAM = reader.GetString("SHILLAKRAKKAM"),
                            ADHICHIMARYADA = reader.GetString("ADHICHIMARYADA"),
                            RAKKAMVADHAVANE = reader.GetString("RAKKAMVADHAVANE"),
                            VAYPARI = reader.GetString("VAYPARI"),
                            STATUS = reader.GetBoolean("STATUS"),
                            VNAME = reader.GetString("VNAME"),
                            VAPNAME = reader.GetString("VAPNAME"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
