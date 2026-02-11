using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_AuctionUserRepository : IGET_AuctionUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AuctionUserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AuctionUser>> GetAll(GET_AuctionUserDomain input)
        {
            List<GET_AuctionUser> list = new List<GET_AuctionUser>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AuctionUser]('"+ input.COMPANYID+ "' , '"+ input.DEPTID+ "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AuctionUser
                        {
                            UFORENAME = reader.GetString("UFORENAME"),
                            UAID = reader.GetString("UAID"),

                        });
                    }
                }
            }
            return list;
        }

        
        
    }
}
