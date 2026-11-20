using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionNosearchRepository : IGET_PRequisitionNosearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PRequisitionNosearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PRequisitionNosearch>> getasync(GET_PRequisitionNosearchDomain gET_PRequisition)
        {
           List<GET_PRequisitionNosearch> list = new List<GET_PRequisitionNosearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionNosearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(gET_PRequisition.PRNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(gET_PRequisition.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(gET_PRequisition.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionNosearch
                        {
                            PRAID = reader.GetString("PRAID"),
                            PRNO = reader.GetString("PRNO"),
                            PRQDATE = reader.GetString("PRQDATE"),
                            PRDAID = reader.GetString("PRDAID"),
                            PRDDESCRIPTION = reader.GetString("PRDDESCRIPTION"),
                            PRDPRODUCT = reader.GetString("PRDPRODUCT"),
                            PRDQUANTITY = reader.GetString("PRDQUANTITY"),
                            PRDUOM = reader.GetString("PRDUOM"),
                           
                        });
                    }
                }

            }
            return list;
        }
    }
}
