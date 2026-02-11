using System.Data;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StandingI_RateA_ShopAlotRepository : IGET_StandingI_RateA_ShopAlotRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StandingI_RateA_ShopAlotRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_StandingI_RateA_ShopAlot>> getasync(GET_StandingI_RateA_ShopAlotDomain input)
        {
            List<GET_StandingI_RateA_ShopAlot> result = new List<GET_StandingI_RateA_ShopAlot>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_StandingI_RateA_ShopAlot]('" + input.SHID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_StandingI_RateA_ShopAlot
                        {
                            PKID = reader.GetString("PKID"),
                            ID = reader.GetString("ID"),
                            NAME = reader.GetString("NAME"),
                            ADHAR = reader.GetString("ADHAR"),
                            MOBILE = reader.GetString("MOBILE"),

                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_StandingI_RateA_ShopAlot>>getkeyword(GET_StandingI_RateA_ShopAlotDomain input)
        {
            List<GET_StandingI_RateA_ShopAlot> result = new List<GET_StandingI_RateA_ShopAlot>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_StandingI_RateA_ShopAlot]('" + input.SHID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_StandingI_RateA_ShopAlot
                        {
                            PKID = reader.GetString("PKID"),
                            ID = reader.GetString("ID"),
                            NAME = reader.GetString("NAME"),
                            ADHAR = reader.GetString("ADHAR"),
                            MOBILE = reader.GetString("MOBILE"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
