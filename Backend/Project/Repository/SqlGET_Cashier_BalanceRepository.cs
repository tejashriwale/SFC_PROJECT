using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Cashier_BalanceRepository : IGET_Cashier_BalanceRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Cashier_BalanceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Cashier_Balance>> GetAll()
        {
            List<GET_Cashier_Balance> list = new List<GET_Cashier_Balance>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Cashier_Balance]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_Cashier_Balance
                        {
                            CashierBalance = reader.GetInt32("CashierBalance"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
