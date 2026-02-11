using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerNameRepository : IGET_FarmerNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerName>> getasync(GET_FarmerNameDomain input)
        {
           List<GET_FarmerName> list = new List<GET_FarmerName>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerName]('" + input.COMPANYID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerName
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO")
                        });
                    }
                }
            }
            return list;
        }
    }
}
