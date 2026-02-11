using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CustomerTranDataRepository : IGET_CustomerTranDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerTranDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CustomerTranData>> getasync(GET_CustomerTranDataDomain input)
        {
            List<GET_CustomerTranData> list = new List<GET_CustomerTranData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CustomerTranData]( '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.REFAID + "' , '" + input.MAID + "')ORDER BY TRNSR ";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerTranData
                        {
                            STATUS = reader.GetBoolean("STATUS"),
                            TRNSID = reader.GetString("TRNSID"),
                            ADVAMT = reader.GetDouble("ADVAMT"),
                            MAID = reader.GetString("MAID"),
                            REFAID = reader.GetString("REFAID"),
                            CBAL = reader.GetDouble("CBAL"),
                            NARR = reader.GetString("NARR"),
                            PAMT = reader.GetDouble("PAMT"),
                            TRNDT = reader.GetString("TRNDT"),
                            TRAID = reader.GetString("TRAID"),
                            TRNSR = reader.GetInt32("TRNSR"),
                            CROPID = reader.GetString("CROPID")


                        });
                    }
                }
            }
            return list;
        }
    }
}
