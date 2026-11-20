using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyaparijamaByVyapariDroDownRepository : IGET_VyaparijamaByVyapariDroDownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyaparijamaByVyapariDroDownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyaparijamaByVyapariDroDown>> getasync(GET_VyaparijamaByVyapariDroDownDomain input)
        {
            List<GET_VyaparijamaByVyapariDroDown> list = new List<GET_VyaparijamaByVyapariDroDown>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyaparijamaByVyapariDroDown](@p0 , @p1  ,@p2 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.VSHORTNAME) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyaparijamaByVyapariDroDown
                        {
                            VPAID = reader.GetString("VPAID"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
                            VNAME = reader.GetString("VNAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VCOMPNAME = reader.GetString("VCOMPNAME"),
                            VGSTIN = reader.GetString("VGSTIN"),
                            TRNDT = reader.GetString("TRNDT"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
