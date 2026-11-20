using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PReturnDataByIDRepository : IGET_PReturnDataByIDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PReturnDataByIDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PReturnDataByID>> getasync(GET_PReturnDataByIDDomain input)
        {
            List<GET_PReturnDataByID> list = new List<GET_PReturnDataByID> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PReturnDataByID] (@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PReturnDataByID
                        {
                            PBNO = reader.GetString("PBNO"),
                            PBDATE = reader.GetString("PBDATE"),
                            POTRNNO = reader.GetString("POTRNNO"),
                            PSELLER = reader.GetString("PSELLER"),
                            PRCONSIGNER = reader.GetString("PRCONSIGNER"),
                            PBREFERENCE = reader.GetString("PBREFERENCE"),
                            PRNARRATION = reader.GetString("PRNARRATION"),
                            PRDATE = reader.GetString("PRDATE"),
                            PRNAMT = reader.GetDouble("PRNAMT"),
                            PRGAMT = reader.GetDouble("PRGAMT"),
                            RTYPE = reader.GetInt32("RTYPE"),
                            PRDAID = reader.GetString("PRDAID"),
                            PRDPRODUCT = reader.GetString("PRDPRODUCT"),
                            PRDUOM = reader.GetString("PRDUOM"),
                            PRDQUANTITY = reader.GetString("PRDQUANTITY"),
                            PRTDAMT = reader.GetDouble("PRTDAMT"),
                            PRDIGST = reader.GetDouble("PRDIGST"),
                            PRDSGST = reader.GetDouble("PRDSGST"),
                            PRDCGST = reader.GetDouble("PRDCGST"),
                            PRDQUANTITYRETURN = reader.GetString("PRDQUANTITYRETURN"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            PRAID = reader.GetString("PRAID"),
                            PAYMENTTYPE = reader.GetString("PAYMENTTYPE")

                        });
                    }
                }
            }
            return list;
        }
    }
}
