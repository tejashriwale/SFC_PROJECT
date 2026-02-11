using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_HIGHESTFRUITRATEEAuctionReposirory : IGET_HIGHESTFRUITRATEEAuctionReposirory
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_HIGHESTFRUITRATEEAuctionReposirory(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_HIGHESTFRUITRATEEAuction>> getasync(GET_HIGHESTFRUITRATEEAuctionDomain input)
        {
            List<GET_HIGHESTFRUITRATEEAuction> list = new List<GET_HIGHESTFRUITRATEEAuction>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_HighestFruitRateAuction]('" + input.DATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_HIGHESTFRUITRATEEAuction
                        {
                            DPKID = reader.GetString("DPKID"),
                            RATE = reader.GetDouble("RATE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            ITEMPHOTO = reader.GetString("ITEMPHOTO"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
