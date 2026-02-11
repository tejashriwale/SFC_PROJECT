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
                command.CommandText = "SELECT * FROM[GET_SCropInformation]('" + input.SRAID + "' , '" + input.DSCAID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
