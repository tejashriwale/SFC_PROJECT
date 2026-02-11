using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CustomerNameRepository : IGET_CustomerNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CustomerName>> getasync(GET_CustomerNameDomain input)
        {
            List<GET_CustomerName> list = new List<GET_CustomerName>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CustomerName]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerName
                        {
                            CAID = reader.GetString("CAID"),
                            CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
}
