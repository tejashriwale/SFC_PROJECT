using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionNumberSearchRepository : IGET_PRequisitionNumberSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PRequisitionNumberSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PRequisitionNumberSearch>> getasync(GET_PRequisitionNosearchDomain gET_PRequisition)
        {
           List<GET_PRequisitionNumberSearch> list = new List<GET_PRequisitionNumberSearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionNumberSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(gET_PRequisition.PRNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(gET_PRequisition.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(gET_PRequisition.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionNumberSearch
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            CLANDMARK = reader.GetString("CLANDMARK"),
                            CACCOUNTNUMBER = reader.GetString("CACCOUNTNUMBER"),
                            CBANKNAME = reader.GetString("CBANKNAME"),
                            CIFSC = reader.GetString("CIFSC"),
                            CBRANCHNAME = reader.GetString("CBRANCHNAME"),
                            PRNO = reader.GetString("PRNO"),
                            PRAID = reader.GetString("PRAID"),
                            STATUS = reader.GetInt32("STATUS"),
                            PRVANDORID = reader.GetString("PRVANDORID")

                        });
                    }
                }
            }
            return list;
        }
    }
}
