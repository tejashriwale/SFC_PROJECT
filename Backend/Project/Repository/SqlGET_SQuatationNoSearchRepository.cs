using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SQuatationNoSearchRepository : IGET_QuatationNoSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SQuatationNoSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SQuatationNoSearch>> getasync(GET_SQuatationNoSearchDomain input)
        {
            List<GET_SQuatationNoSearch> list = new List<GET_SQuatationNoSearch>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SQuatationNoSearch]('" + input.QNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SQuatationNoSearch
                        {
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            QQUANTITY = reader.GetString("QQUANTITY"),
                            QRATE = reader.GetDouble("QRATE"),
                            QTAMT = reader.GetDouble("QTAMT"),
                            QIGST = reader.GetDouble("QIGST"),
                            QSGST = reader.GetDouble("QSGST"),
                            QCGST = reader.GetDouble("QCGST"),
                            QCESS = reader.GetDouble("QCESS"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            SQCAID = reader.GetString("SQCAID"),
                            SQPRODUCT = reader.GetString("SQPRODUCT"),
                            SQTRANSPORTID = reader.GetString("SQTRANSPORTID"),
                            SQUOM = reader.GetString("SQUOM"),
                            TransporterTitle = reader.GetString("TransporterTitle"),
                            HSNCODE = reader.GetInt32("HSNCODE"),

                        });
                    }
                }

            } 
            return list;
        }
    }
}
