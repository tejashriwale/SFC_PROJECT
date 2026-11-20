using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CheckTranspoterTaidRepository : IGET_CheckTranspoterTaidRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CheckTranspoterTaidRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CheckTranspoterTaid>> getasync(GET_CheckTranspoterTaidDomain input)
        {
            List<GET_CheckTranspoterTaid> list = new List<GET_CheckTranspoterTaid>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CheckTranspoterTaid](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.TAID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CheckTranspoterTaid
                        {
                            TAID = reader.GetString("TAID"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
