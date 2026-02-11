using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VBillChargesBillRepository : IGET_VBillChargesBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VBillChargesBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_VBillChargesBill>> getasync(GET_VBillChargesBillDomain input)
        {
            List<GET_VBillChargesBill> list = new List<GET_VBillChargesBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VBillChargesBill]('" + input.BAID + "' , '" + input.DATE + "' , '" + input.BILLNO + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VBillChargesBill
                        {
                            SEVAID = reader.GetString("SEVAID"),
                            TotalAmount = reader.GetDecimal("TotalAmount"),
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                            RateTypeTitle = reader.GetString("RateTypeTitle"),
                            RateTypeValue = reader.GetString("RateTypeValue"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
