using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StandingI_RateA_VyapariRepository : IGET_StandingI_RateA_VyapariRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StandingI_RateA_VyapariRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_StandingI_RateA_Vyapari>> getasync(GET_StandingI_RateA_VyapariDomain input)
        {
           List<GET_StandingI_RateA_Vyapari> result = new List<GET_StandingI_RateA_Vyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_StandingI_RateA_Vyapari]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_StandingI_RateA_Vyapari
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

        public async Task<List<GET_StandingI_RateA_Vyapari>> getkeyword(GET_StandingI_RateA_VyapariDomain input)
        {
            List<GET_StandingI_RateA_Vyapari> result = new List<GET_StandingI_RateA_Vyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_StandingI_RateA_Vyapari]('" + input.VPAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_StandingI_RateA_Vyapari
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
