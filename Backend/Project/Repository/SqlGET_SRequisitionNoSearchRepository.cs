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
                command.CommandText = "SELECT * FROM [GET_SRequisitionNoSearch] (@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRNO) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
