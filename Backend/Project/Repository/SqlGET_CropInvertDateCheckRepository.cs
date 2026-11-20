using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropInvertDateCheckRepository : IGET_CropInvertDateCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropInvertDateCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropInvertDateCheck>> getasync(GET_CropInvertDateCheckDomain input)
        {
            List<GET_CropInvertDateCheck> list = new List<GET_CropInvertDateCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropInvertDateCheck](@p0 , @p1 , @p2 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.SHSDATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.CGRADE) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertDateCheck
                        {
                            DateExists = reader.GetInt32("DateExists"),
                         
                        });
                    }
                }
            }
            return list;
        }
    }
}
