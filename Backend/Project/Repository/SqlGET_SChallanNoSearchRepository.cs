using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SChallanNoSearchRepository : IGET_SChallanNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SChallanNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SChallanNoSearch>> getasync(GET_SChallanNoSearchDomain input)
        {
            List<GET_SChallanNoSearch> list = new List<GET_SChallanNoSearch> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SChallanNoSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SCTRNNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SChallanNoSearch
                        {
                            SCDQUANTITY = reader.GetString("SCDQUANTITY"),
                            SCDRATE = reader.GetDouble("SCDRATE"),
                            SCDTAMT = reader.GetDouble("SCDTAMT"),
                            SCDIGST = reader.GetDouble("SCDIGST"),
                            SCDCGST = reader.GetDouble("SCDCGST"),
                            SCTRNNO = reader.GetString("SCTRNNO"),
                            SCSELLER = reader.GetString("SCSELLER"),
                            SCVNO = reader.GetString("SCVNO"),
                            SCPOSUPPLY = reader.GetString("SCPOSUPPLY"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            SCDSGST = reader.GetDouble("SCDSGST"),
                            SCDTAXABLE = reader.GetString("SCDTAXABLE"),
                            SCCONSIGNER = reader.GetString("SCCONSIGNER"),
                            Trnasporter = reader.GetString("Trnasporter"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            SCDPRODUCT = reader.GetString("SCDPRODUCT"),
                            SCDUOM = reader.GetString("SCDUOM"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            CSTATE = reader.GetString("CSTATE"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
