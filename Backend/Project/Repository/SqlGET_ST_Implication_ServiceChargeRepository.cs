using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ST_Implication_ServiceChargeRepository : IGET_ST_Implication_ServiceChargeRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ST_Implication_ServiceChargeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ST_Implication_ServiceCharge>> GetAll()
        {
            List<GET_ST_Implication_ServiceCharge> list = new List<GET_ST_Implication_ServiceCharge> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ST_Implication_ServiceCharge]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ST_Implication_ServiceCharge
                        {
                            CNT = reader.GetInt32("CNT")
                        });
                    }
                }
            }
            return list;
        }
    }
}
