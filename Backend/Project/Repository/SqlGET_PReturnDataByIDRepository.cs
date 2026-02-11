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
                command.CommandText = "SELECT * FROM [GET_PReturnDataByID] ('" + input.PRAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
