using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerBillDataRepository : IGET_FarmerBillDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerBillDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerBillData>> getasync(GET_FarmerBillDataDomain input)
        {
            List<GET_FarmerBillData> list = new List<GET_FarmerBillData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerBillData]('" + input.COMPANYID + "','" + input.DEPTID + "','" + input.MAID + "','" + input.CROPID + "')ORDER BY TRNSR DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerBillData
                        {
                            TRNSR = reader.GetInt32("TRNSR"),
                            ADVAMT = reader.GetDouble("ADVAMT"),
                            CBAL = reader.GetDouble("CBAL"),
                            CROPID = reader.GetString("CROPID"),
                            FNAME = reader.GetString("FNAME"),
                            ITEMNE = reader.GetString("ITEMNE"),
                            MAID = reader.GetString("MAID"),
                            REFAID = reader.GetString("REFAID"),
                            TRAID = reader.GetString("TRAID"),
                            TRNDT = reader.GetString("TRNDT"),
                            PKID = reader.GetString("PKID")



                        });

                    }
                }
            }
            return list;
        }
    }
}
