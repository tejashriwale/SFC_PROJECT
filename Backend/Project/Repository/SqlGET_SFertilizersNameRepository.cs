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
                command.CommandText = "SELECT * FROM [GET_SFertilizersName] ('" + input.SRAID + "' , '" + input.DSFID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
