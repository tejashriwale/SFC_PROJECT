using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_AllTranDataCustomerRepository : IGET_AllTranDataCustomerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AllTranDataCustomerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AllTranDataCustomer>> getasync(GET_AllTranDataCustomerDomain input)
        {
            List<GET_AllTranDataCustomer> list = new List<GET_AllTranDataCustomer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AllTranDataCustomer]('" + input.COMPANYID + "' , '" + input.DEPTID + "'  , '" + input.MAID + "'  )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AllTranDataCustomer
                        {
                           ADVAMT = reader.GetDouble("ADVAMT"),
                           CBAL = reader.GetDouble("CBAL"),
                           MAID = reader.GetString("MAID"),
                           PAMT = reader.GetDouble("PAMT"),
                           REFAID = reader.GetString("REFAID"),
                           STATUS = reader.GetBoolean("STATUS"),
                           TRAID = reader.GetString("TRAID"),
                           TRNSR = reader.GetInt32("TRNSR"),
                           TRNTID  = reader.GetString("TRNTID")


                        });
                    }
                }
            }
            return list;
        }
    }
}
