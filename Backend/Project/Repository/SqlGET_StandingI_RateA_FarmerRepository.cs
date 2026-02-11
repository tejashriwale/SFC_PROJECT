using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StandingI_RateA_FarmerRepository : IGET_StandingI_RateA_FarmerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StandingI_RateA_FarmerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_StandingI_RateA_Farmer>> getasync(GET_StandingI_RateA_FarmerDomain input)
        {
            List<GET_StandingI_RateA_Farmer> result = new List<GET_StandingI_RateA_Farmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_StandingI_RateA_Farmer]('" + input.MAID + "' ,'"+input.KEYWORD+"', '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                 
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_StandingI_RateA_Farmer
                        {
                          ADHAR = reader.GetString("ADHAR"),
                            ID = reader.GetString("ID"),
                            MOBILE = reader.GetString("MOBILE"),
                            NAME = reader.GetString("NAME"),
                            PKID = reader.GetString("PKID")
                        });
                    }

                }
            }
            return result;
        }

        public async Task<List<GET_StandingI_RateA_Farmer>> getkeyword(GET_StandingI_RateA_FarmerDomain input)
        {
            List<GET_StandingI_RateA_Farmer> result = new List<GET_StandingI_RateA_Farmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_StandingI_RateA_Farmer]('" + input.MAID + "' ,'" + input.KEYWORD + "', '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_StandingI_RateA_Farmer
                        {
                            ADHAR = reader.GetString("ADHAR"),
                            ID = reader.GetString("ID"),
                            MOBILE = reader.GetString("MOBILE"),
                            NAME = reader.GetString("NAME"),
                            PKID = reader.GetString("PKID")
                        });
                    }

                }
            }
            return result;
        }
    }
}
