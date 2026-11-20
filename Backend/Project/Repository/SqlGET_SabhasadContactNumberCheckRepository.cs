using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SabhasadContactNumberCheckRepository : IGET_SabhasadContactNumberCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SabhasadContactNumberCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SabhasadContactNumberCheck>> getasync(GET_SabhasadContactNumberCheckDomain input)
        {
            List<GET_SabhasadContactNumberCheck> result = new List<GET_SabhasadContactNumberCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SabhasadContactNumberCheck](@p0 ,  @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CONTACTNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_SabhasadContactNumberCheck
                        {
                            SABHASADNAME = reader.GetString("SABHASADNAME"),
                            SRAID = reader.GetString("SRAID"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER")
                        });
                    }
                }

            }
            return result;
        }
    }
}
