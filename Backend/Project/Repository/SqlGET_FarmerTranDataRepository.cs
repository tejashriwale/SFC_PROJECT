using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerTranDataRepository : IGET_FarmerTranDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerTranDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerTranData>> getasync(GET_FarmerTranDataDomain input)
        {
            List<GET_FarmerTranData> result = new List<GET_FarmerTranData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerTranData]('" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.REFAID + "' , '" + input.MAID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_FarmerTranData
                        {
                           STATUS = reader.GetBoolean("STATUS"),
                           ADVAMT = reader.GetDouble("ADVAMT"),
                           TRAID = reader.GetString("TRAID"),
                           REFAID = reader.GetString("REFAID"),
                           CBAL = reader.GetDouble("CBAL"),
                           MAID = reader.GetString("MAID"),
                           CROPID = reader.GetString("CROPID"),
                           TRNDT = reader.GetString("TRNDT"),
                           TRNSID = reader.GetString("TRNSID"),
                            NARR = reader.GetString("NARR"),
                           TRNSR = reader.GetInt32("TRNSR"),
                           PAMT = reader.GetDouble("PAMT")

                        });
                    }
                }
            }
            return result;
        }
    }
}
