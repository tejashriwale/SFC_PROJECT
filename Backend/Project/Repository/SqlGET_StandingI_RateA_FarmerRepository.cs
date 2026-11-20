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
                command.CommandText = "SELECT  * FROM [GET_StandingI_RateA_Farmer](@p0 ,@p1, @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT * FROM [GET_StandingI_RateA_Farmer](@p0 ,@p1, @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
