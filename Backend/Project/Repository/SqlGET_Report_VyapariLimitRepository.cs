using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Report_VyapariLimitRepository : IGET_Report_VyapariLimitRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_VyapariLimitRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Report_VyapariLimit>> getasync(GET_Report_VyapariLimitDomain input)
        {
            List<GET_Report_VyapariLimit> list = new List<GET_Report_VyapariLimit>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_VyapariLimit](@p0, @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Report_VyapariLimit
                        {

                            VNAME = reader.GetString("VNAME"),
                            VSAID = reader.GetString("VSAID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            AMOUNT = reader.GetDouble("AMOUNT"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
