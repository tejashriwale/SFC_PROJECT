using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_POrderNoSearchRepository : IGET_POrderNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_POrderNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_POrderNoSearch>> getasync(GET_POrderNoSearchDomain input)
        {
            List<GET_POrderNoSearch> list = new List<GET_POrderNoSearch> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_POrderNoSearch]('" + input.POTRANNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_POrderNoSearch
                        {
                            POAID = reader.GetString("POAID"),
                            POTRANNO = reader.GetString("POTRANNO"),
                            POVENDORID = reader.GetString("POVENDORID"),
                            POCONSIGNER = reader.GetString("POCONSIGNER"),
                            POPOSUPPLY = reader.GetString("POPOSUPPLY"),
                            POQUANTITY = reader.GetString("POQUANTITY"),
                            PODUEDATE = reader.GetString("PODUEDATE"),
                            PONARRATION = reader.GetString("PONARRATION"),
                            POGAMT = reader.GetDouble("POGAMT"),
                            POPDUEDAYS = reader.GetString("POPDUEDAYS"),
                            PODDATE = reader.GetString("PODDATE"),
                            PODESCRIPTION = reader.GetString("PODESCRIPTION"),
                            POPLACE = reader.GetString("POPLACE"),
                            POPTERMS = reader.GetString("POPTERMS"),
                            POTERMANDCONDITION = reader.GetString("POTERMANDCONDITION"),
                            PODATE = reader.GetString("PODATE"),
                            PNODAYS = reader.GetInt32("PNODAYS"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            PONMT = reader.GetDouble("PONMT"),
                            PODAID = reader.GetString("PODAID"),
                            PODPRODUCT = reader.GetString("PODPRODUCT"),
                            PODUOM = reader.GetString("PODUOM"),
                            PODRATE = reader.GetDouble("PODRATE"),
                            PODQUANTITY = reader.GetString("PODQUANTITY"),
                            PODTAMT = reader.GetDouble("PODTAMT"),
                            PODIGST = reader.GetDouble("PODIGST"),
                            PODSGST = reader.GetDouble("PODSGST"),
                            PODCGST = reader.GetDouble("PODCGST"),
                            PODTOTAL = reader.GetDouble("PODTOTAL"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            HSNCODE = reader.GetInt32("HSNCODE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
