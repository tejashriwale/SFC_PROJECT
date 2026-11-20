using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SCropInformationRepository : IGET_SCropInformationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SCropInformationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SCropInformation>> getasync(GET_SCropInformationDomain input)
        {
            List<GET_SCropInformation> list = new List<GET_SCropInformation>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM[GET_SCropInformation](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DSCAID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SCropInformation
                        {
                            AREA = reader.GetString("AREA"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            DSCAID = reader.GetString("DSCAID"),
                            ECROP = reader.GetString("ECROP"),
                            GROWINGPERIOD = reader.GetString("GROWINGPERIOD"),
                            HDURATION = reader.GetString("HDURATION"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            SRAID = reader.GetString("SRAID"),
                            CTYPE = reader.GetString("CTYPE"),
                            CNAME = reader.GetString("CNAME"),
                            NTYPE = reader.GetString("NTYPE"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
