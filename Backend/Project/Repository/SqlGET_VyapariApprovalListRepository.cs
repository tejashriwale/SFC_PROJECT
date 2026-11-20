using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariApprovalListRepository : IGET_VyapariApprovalListRepository
    {
        private readonly ApplicationDbContext  dbContext;
        public SqlGET_VyapariApprovalListRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariApprovalList>> getasync(GET_VyapariApprovalListDomain input)
        {
            List<GET_VyapariApprovalList> result = new List<GET_VyapariApprovalList>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariApprovalList]( @p0 , @p1 , @p2  )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VYAPARI) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariApprovalList
                        {
                            MAID = reader.GetString("MAID"),
                            WEIGHTLESS = reader.GetDouble("WEIGHTLESS"),
                            WEIGHT = reader.GetString("WEIGHT"),
                            JAALI = reader.GetInt32("JAALI"),
                            RATE = reader.GetDouble("RATE"),
                            TOKEN = reader.GetString("TOKEN"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            WEIGHTMAXMIN = reader.GetBoolean("WEIGHTMAXMIN"),
                            APKID = reader.GetString("APKID"),
                            DPKID = reader.GetString("DPKID"),
                            FNAME = reader.GetString("FNAME"),
                            ISAPPORVED = reader.GetBoolean("ISAPPORVED"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                           

                        });
                    }
                }
            }
            return result;
        }
    }
}
