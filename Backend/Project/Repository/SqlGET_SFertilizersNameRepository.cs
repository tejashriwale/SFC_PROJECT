using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SFertilizersNameRepository : IGET_SFertilizersNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SFertilizersNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SFertilizersName>> getasync(GET_SFertilizersNameDomain input)
        {
            List<GET_SFertilizersName> list = new List<GET_SFertilizersName>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SFertilizersName] (@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DSFID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SFertilizersName
                        {
                           DSFID = reader.GetString("DSFID"),
                           SRAID = reader.GetString("SRAID"),
                           COST = reader.GetDouble("COST"),
                           FERTILIZERSNAME = reader.GetString("FERTILIZERSNAME"),
                           QUANTITY = reader.GetDouble("QUANTITY"),
                           FTYPE = reader.GetString("FTYPE"),

                            FNAME = reader.GetString("FNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED")

                        });
                    }
                }
            }
            return list;
        }
    }
}
