using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CheckSRNO_ExistRepository : IGET_CheckSRNO_ExistRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CheckSRNO_ExistRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CheckSRNO_Exist>> getasync(GET_CheckSRNO_ExistDomain input)
        {
            List<GET_CheckSRNO_Exist> list = new List<GET_CheckSRNO_Exist>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CheckSRNO_Exist]('" + input.SRNO + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CheckSRNO_Exist
                        {
                            SRNO = reader.GetString("SRNO"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
