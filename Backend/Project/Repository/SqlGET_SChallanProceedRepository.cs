using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SChallanProceedRepository : IGET_SChallanProceedRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SChallanProceedRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SChallanProceed>> getasync(GET_SChallanProceedDomain input)
        {
            List<GET_SChallanProceed> result = new List<GET_SChallanProceed>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SChallanProceed](@p0 ,  @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_SChallanProceed
                        {
                          SCCONSIGNER = reader.GetString("SCCONSIGNER"),
                          BATCHNO = reader.GetString("BATCHNO"),
                          CCONTACTPERSONNAME = reader.GetString("CCONTACTPERSONNAME"),
                          CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                          CSTATE  = reader.GetString("CSTATE"),
                          EMAIL = reader.GetString("EMAIL"),
                          EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                          PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                          PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                          SCDQUANTITY = reader.GetString("SCDQUANTITY"),
                          SCDTAMT = reader.GetDouble("SCDTAMT"),
                          SCDRATE = reader.GetDouble("SCDRATE"),
                          SCDTAXABLE = reader.GetString("SCDTAXABLE"),
                          SCGAMT = reader.GetDouble("SCGAMT"),
                          SCNAMT = reader.GetDouble("SCNAMT"),
                          SSTATENAME = reader.GetString("SSTATENAME"),
                          HSNCODE = reader.GetInt32("HSNCODE"),
                          SCDCGST = reader.GetDouble("SCDCGST"),
                          SCDIGST = reader.GetDouble("SCDIGST"),
                          SCDPRODUCT = reader.GetString("SCDPRODUCT"),
                          SCDSGST = reader.GetDouble("SCDSGST"),
                          SCDUOM = reader.GetString("SCDUOM"),
                          SCPOSUPPLY = reader.GetString("SCPOSUPPLY"),
                          SCSELLER = reader.GetString("SCSELLER"),
                          SCTRNNO = reader.GetString("SCTRNNO"),
                          SCVNO = reader.GetString("SCVNO"),
                          UOMTITLE = reader.GetString("UOMTITLE"),
                            TRANSPORTER = reader.GetString("TRANSPORTER"),

                        });
                    }
                }

            }
            return result;
        }
    }
}
