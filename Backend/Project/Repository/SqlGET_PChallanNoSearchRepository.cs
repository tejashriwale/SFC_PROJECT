using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PChallanNoSearchRepository : IGET_PChallanNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PChallanNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PChallanNoSearch>> getasync(GET_PChallanNoSearchDomain input)
        {
            List<GET_PChallanNoSearch> list = new List<GET_PChallanNoSearch> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_PChallanNoSearch]('" + input.PCTRNNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PChallanNoSearch
                        {
                            PCAID = reader.GetString("PCAID"),
                            DPCAID = reader.GetString("DPCAID"),
                            PCDPRODUCT = reader.GetString("PCDPRODUCT"),
                            PCDUOM = reader.GetString("PCDUOM"),
                            PCDQUANTITY = reader.GetString("PCDQUANTITY"),
                            PCDRATE = reader.GetDouble("PCDRATE"),
                            PCDTAMT = reader.GetDouble("PCDTAMT"),
                            PCDTAXABLE = reader.GetString("PCDTAXABLE"),
                            PCDIGST = reader.GetDouble("PCDIGST"),
                            PCDSGST = reader.GetDouble("PCDSGST"),
                            PCDCGST = reader.GetDouble("PCDCGST"),
                            PCTRNNO = reader.GetString("PCTRNNO"),
                            PCSELLER = reader.GetString("PCSELLER"),
                            PCCONSIGNER = reader.GetString("PCCONSIGNER"),
                            PCVNO = reader.GetString("PCVNO"),
                            PCPOSUPPLY = reader.GetString("PCPOSUPPLY"),
                            PCNARRATION = reader.GetString("PCNARRATION"),
                            PCDATE = reader.GetString("PCDATE"),
                            PCNAMT = reader.GetDouble("PCNAMT"),
                            PCGAMT = reader.GetDouble("PCGAMT"),
                            POREFF = reader.GetString("POREFF"),
                            STATUSID = reader.GetString("STATUSID"),
                            PRODUCTTITLE = reader.GetString("PRODUCTTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            PAYMENTMODE = reader.GetString("PAYMENTMODE"),
                            PAYMENTYPE = reader.GetString("PAYMENTYPE"),
                            GRNDCHALLANQUANTITY = reader.GetDouble("GRNDCHALLANQUANTITY"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
