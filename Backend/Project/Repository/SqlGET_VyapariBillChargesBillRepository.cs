using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariBillChargesBillRepository : IGET_VyapariBillChargesBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariBillChargesBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariBillChargesBill>> getasync(GET_VyapariBillChargesBillDomain input)
        {
            List<GET_VyapariBillChargesBill> list = new List<GET_VyapariBillChargesBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariBillChargesBill]('" + input.BAID + "' , '"+input.DATE+"' , '"+input.BILLNO+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariBillChargesBill
                        {
                            SEVAID = reader.GetString("SEVAID"),
                            TotalAmount = reader.GetDecimal ("TotalAmount"),
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
