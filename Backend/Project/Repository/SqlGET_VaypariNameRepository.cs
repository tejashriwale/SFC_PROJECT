using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VaypariNameRepository : IGET_VaypariNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VaypariNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VaypariName>> getasync(GET_VaypariNameDomain input)
        {
            List<GET_VaypariName> list = new List<GET_VaypariName> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VaypariName](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_VaypariName
                        {
                            VPAID = reader.GetString("VPAID"),
                            VNAME = reader.GetString("VNAME"),
                            VIFSCCODE = reader.GetString("VIFSCCODE"),
                            VACCOUNTNUMBER = reader.GetString("VACCOUNTNUMBER"),
                            VACCOUNTNAME = reader.GetString("VACCOUNTNAME"),
                            VBANKNAME = reader.GetString("VBANKNAME"),
                            VMOBILE = reader.GetString("VMOBILE"),
                            VFULLNBAME = reader.GetString("VFULLNBAME")
                        });
                    }
                }
            }
            return list;
        }
    }
}
