using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PBillDetailsDataRepository : IGET_PBillDetailsDataRepository
    {
        private ApplicationDbContext dbContext;
        public SqlGET_PBillDetailsDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PBillDetailsData>> getasync(GET_PBillDetailsDataDomain input)
        {
            List<GET_PBillDetailsData> list = new List<GET_PBillDetailsData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_PBillDetailsData](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PBAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_PBillDetailsData()
                        {
                            PBAID = reader.GetString("PBAID"),
                            PBDAID = reader.GetString("PBDAID"),
                            PBPRODUCT = reader.GetString("PBPRODUCT"),
                            PBDUOM = reader.GetString("PBDUOM"),
                            PBDQUANTITY = reader.GetString("PBDQUANTITY"),
                            PBDRATE = reader.GetDouble("PBDRATE"),
                            PBDTAMT = reader.GetDouble("PBDTAMT"),
                            PBDIGST = reader.GetDouble("PBDIGST"),
                            PBDSGST = reader.GetDouble("PBDSGST"),
                            PBDCGST = reader.GetDouble("PBDCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
