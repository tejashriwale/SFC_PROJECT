using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariApprovalByIDRepository : IGET_VyapariApprovalByIDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariApprovalByIDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariApprovalByID>> getasync(GET_VyapariApprovalByIDDomain input)
        {
           List<GET_VyapariApprovalByID> result = new List<GET_VyapariApprovalByID>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariApprovalByID](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DPKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariApprovalByID
                        {
                            APKID = reader.GetString("APKID"),
                            WEIGHTLESS = reader.GetDouble("WEIGHTLESS"),
                            WEIGHTMAXMIN = reader.GetBoolean("WEIGHTMAXMIN"),
                            TOKEN = reader.GetString("TOKEN"),
                            RATE = reader.GetDouble("RATE"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            DPKID = reader.GetString("DPKID"),
                            FNAME = reader.GetString("FNAME"),
                            MAID = reader.GetString("MAID"),
                            JAALI = reader.GetInt32("JAALI"),
                            WEIGHT = reader.GetString("WEIGHT")


                        });
                    }
                }
            }
            return result;
        }
    }
}
