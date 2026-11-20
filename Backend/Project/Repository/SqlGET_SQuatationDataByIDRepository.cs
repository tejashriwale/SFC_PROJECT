using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SQuatationDataByIDRepository : IGET_SQuatationDataByIDRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SQuatationDataByIDRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SQuatationDataByID>> getasync(GET_SQuatationDataByIDDomain input)
        {
            List<GET_SQuatationDataByID> list = new List<GET_SQuatationDataByID> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SQuatationDataByID](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SQAMAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SQuatationDataByID
                        {
                            CAID = reader.GetString("CAID"),
                            SQCAID = reader.GetString("SQCAID"),
                            SQNO = reader.GetString("SQNO"),
                            SQMNAMT = reader.GetDouble("SQMNAMT"),
                            SQGAMT = reader.GetDouble("SQGAMT"),
                            SQTRANSPORTID = reader.GetString("SQTRANSPORTID"),
                            SQPRODUCT = reader.GetString("SQPRODUCT"),
                            SQQUANTITY = reader.GetString("SQQUANTITY"),
                            SQUOM = reader.GetString("SQUOM"),
                            SQRATE = reader.GetDouble("SQRATE"),
                            SQTAMT = reader.GetDouble("SQTAMT"),
                            SQIGST = reader.GetDouble("SQIGST"),
                            SQSGST = reader.GetDouble("SQSGST"),
                            SQCGST = reader.GetDouble("SQCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            SQTAXBALEAMOUNT = reader.GetDouble("SQTAXBALEAMOUNT"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            EMAIL = reader.GetString("EMAIL"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            TRANSPORTNAME = reader.GetString("TRANSPORTNAME"),
                        });
                    }
                }

            }
            return list;
        }
    }
}
