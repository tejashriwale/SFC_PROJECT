using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlStatePincodeRepository : IStatePincodeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlStatePincodeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<StatePincode>> getasync(StatePincodeDomain statePincodeDomain)
        {
            List<StatePincode> result = new List<StatePincode>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from[GET_StatePincode](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(statePincodeDomain.SPINCODE) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader .ReadAsync())
                    {
                        result.Add(new StatePincode
                        {
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            said = reader.GetString("said"),
                            SSTATECODE = reader.GetString("SSTATECODE"),
                        });
                    }
                }
            }
            return result;
            
        }
    }
}
