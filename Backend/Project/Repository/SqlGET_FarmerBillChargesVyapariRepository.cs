using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_FarmerBillChargesVyapariRepository : IGET_FarmerBillChargesVyapariRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerBillChargesVyapariRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerBillChargesVyapari>> getasync(GET_FarmerBillChargesVyapariDomain input)
        {
            List<GET_FarmerBillChargesVyapari> list = new List<GET_FarmerBillChargesVyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM[GET_FarmerBillChargesVyapari]( '" + input.BAID + "' , '"+input.BILLNO+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerBillChargesVyapari
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
