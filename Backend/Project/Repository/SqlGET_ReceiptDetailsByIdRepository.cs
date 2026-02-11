using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ReceiptDetailsByIdRepository : IGET_ReceiptDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ReceiptDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ReceiptDetailsById>> getasync(GET_ReceiptDetailsByIdDomain input)
        {
            List<GET_ReceiptDetailsById> result = new List<GET_ReceiptDetailsById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ReceiptDetailsById]('" + input.BAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_ReceiptDetailsById
                        {
                            BAID = reader.GetString("BAID"),
                            BDAID = reader.GetString("BDAID"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CROPTYPE = reader.GetString("CROPTYPE"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            JALI = reader.GetInt32("JALI"),
                            RATE = reader.GetDouble("RATE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX")

                        });
                    }
                }
            }
            return result;
        }
    }
}
