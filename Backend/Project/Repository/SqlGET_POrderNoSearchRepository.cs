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
                command.CommandText = "SELECT * FROM [GET_POrderNoSearch](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.POTRANNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
