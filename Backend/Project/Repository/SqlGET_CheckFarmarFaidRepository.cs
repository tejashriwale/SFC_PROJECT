using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CheckFarmarFaidRepository : IGET_CheckFarmarFaidRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CheckFarmarFaidRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CheckFarmarFaid>> getasync(GET_CheckFarmarFaidDomain input)
        {
            List<GET_CheckFarmarFaid> list = new List<GET_CheckFarmarFaid>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CheckFarmarFaid]('" + input.FAID + "'    )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CheckFarmarFaid
                        {
                            FAID = reader.GetString("FAID"),
                         

                        });
                    }
                }
            }
            return list;
        }
    }

}
