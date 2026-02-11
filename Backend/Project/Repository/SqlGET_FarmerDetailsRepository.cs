using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerDetailsRepository : IGET_FarmerDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerDetails>> getasync(GET_FarmerDetailsDomain input)
        {
            List<GET_FarmerDetails> list = new List<GET_FarmerDetails>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerDetails]('" + input.KEYWORD + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerDetails
                        {

                            FAID = reader.GetString("FAID"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FCITY = reader.GetString("FCITY"),
                            DISTRICT = reader.GetString("DISTRICT"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
