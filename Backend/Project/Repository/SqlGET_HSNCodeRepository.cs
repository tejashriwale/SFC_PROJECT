using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HSNCodeRepository : IGET_HSNCodeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_HSNCodeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_HSNCode>> getasync(HSNCodeDomain hSNCode)
        {
           List<GET_HSNCode> list = new List<GET_HSNCode>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_HSNCode](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(hSNCode.KEYWORD) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_HSNCode
                        {
                            HAID = reader.GetString("HAID"),
                            HCODE = reader.GetString("HCODE"),
                            HTYPE = reader.GetString("HTYPE"),
                            HDESCRIPTION = reader.GetString("HDESCRIPTION"),
                            HIGST = reader.GetDouble("HIGST"),
                            HSGST = reader.GetDouble("HSGST"),
                            HCGST = reader.GetDouble("HCGST"),
                            HCESS = reader.GetDouble("HCESS"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
