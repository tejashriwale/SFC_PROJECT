using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_AllTransectionsDataRepository : IGET_AllTransectionsDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AllTransectionsDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AllTransectionsData>> getasync(GET_AllTransectionsDataDomain input)
        {
            List<GET_AllTransectionsData> list = new List<GET_AllTransectionsData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_AllTransectionsData]('" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.REFAID + "', '" + input.MAID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AllTransectionsData
                        {
                           MAID = reader.GetString("MAID"),
                           ADVAMT = reader.GetDouble("ADVAMT"),
                           REFAID = reader.GetString("REFAID"),
                           TRNSR = reader.GetInt32("TRNSR"),
                           CBAL = reader.GetDouble("CBAL"),
                           PAMT = reader.GetDouble("PAMT"),
                           TRAID = reader.GetString("TRAID"),
                           TRNTID = reader.GetString("TRNTID"),
                           STATUS = reader.GetBoolean("STATUS")

                        });
                    }
                }
            }
            return list;
        }
    }
}
