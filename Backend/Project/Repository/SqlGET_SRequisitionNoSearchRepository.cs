using System.Data;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SRequisitionNoSearchRepository : IGET_SRequisitionNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SRequisitionNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SRequisitionNoSearch>> getasync(GET_SRequisitionNoSearchDomain input)
        {
            List<GET_SRequisitionNoSearch> list = new List<GET_SRequisitionNoSearch> ();
            using(var command  = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SRequisitionNoSearch] ('" + input.SRNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                { 
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SRequisitionNoSearch
                        {
                            SRDQUANTITY = reader.GetString("SRDQUANTITY"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            SRDPRODUCT = reader.GetString("SRDPRODUCT"),
                            SRDUOM = reader.GetString("SRDUOM"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            SRVANDORID = reader.GetString("SRVANDORID"),
                            CSTATE = reader.GetString("CSTATE"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            SRNO = reader.GetString("SRNO")
                        });
                    }
                }
            }
            return list;
        }
    }
}
