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
                command.CommandText = "SELECT  * FROM [GET_StandingI_RateA_Vyapari](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT * FROM [GET_StandingI_RateA_Vyapari](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
