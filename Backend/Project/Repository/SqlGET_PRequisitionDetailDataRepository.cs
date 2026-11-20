using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionDetailDataRepository : IGET_PRequisitionDetailDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PRequisitionDetailDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PRequisitionDetailData>> getasync(GET_PRequisitionDetailDataDomain input)
        {
           List<GET_PRequisitionDetailData> list = new List<GET_PRequisitionDetailData> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionDetailData](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync ())
                {
                    while(await  reader.ReadAsync ())
                    {
                        list.Add(new GET_PRequisitionDetailData
                        {
                            PRDAID = reader.GetString("PRDAID"),
                            PRAID = reader.GetString("PRAID"),
                            PRDDESCRIPTION = reader.GetString("PRDDESCRIPTION"),
                            PRDQUANTITY = reader.GetString("PRDQUANTITY"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            PRDPRODUCT = reader.GetString("PRDPRODUCT"),
                            PRDUOM = reader.GetString("PRDUOM"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            PRBASEQTY = reader.GetInt32("PRBASEQTY"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
