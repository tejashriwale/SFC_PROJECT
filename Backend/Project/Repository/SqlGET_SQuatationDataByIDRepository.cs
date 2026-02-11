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
                command.CommandText = "SELECT * FROM [GET_SQuatationDataByID]('" + input.SQAMAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
