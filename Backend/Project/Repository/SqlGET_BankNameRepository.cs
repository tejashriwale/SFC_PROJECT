using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_BankNameRepository : IGET_BankNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_BankNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_BankName>> getasync(GET_BankNameDomain input)
        {
            List<GET_BankName> result = new List<GET_BankName>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_BankName]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_BankName
                        {
                            BANKID = reader.GetString("BANKID"),
                            BANKNAME = reader.GetString("BANKNAME")
                        });
                    }
                }
            }
            return result;
        }
    }
}
