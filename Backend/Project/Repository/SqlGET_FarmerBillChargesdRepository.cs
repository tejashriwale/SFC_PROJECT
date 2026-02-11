using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerBillChargesdRepository : IGET_FarmerBillChargesdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerBillChargesdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerBillCharges>> getasync(GET_FarmerBillChargesDomain input)
        {
            List<GET_FarmerBillCharges> list = new List<GET_FarmerBillCharges>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_FarmerBillCharges]('" + input.BAID + "'  , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerBillCharges
                        {
                            SEVA = reader.GetString("SEVA"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                           
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                           
                        });
                    }

                }
            }
            return list;
        }
    }
}
