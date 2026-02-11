using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SQuatationForProceedRepository : IGET_SQuatationForProceedRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SQuatationForProceedRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SQuatationForProceed>> getasync(GET_SQuatationForProceedDomain input)
        {
            List<GET_SQuatationForProceed> list = new List<GET_SQuatationForProceed>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SQuatationForProceed]('" + input.SQNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SQuatationForProceed
                        {
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            QQUANTITY = reader.GetString("QQUANTITY"),
                            QCGST = reader.GetDouble("QCGST"),
                            QSGST = reader.GetDouble("QSGST"),
                            QIGST = reader.GetDouble("QIGST"),
                            QRATE = reader.GetDouble("QRATE"),
                            QTAMT = reader.GetDouble("QTAMT"),
                            SQNO = reader.GetString("SQNO"),
                            SQCAID = reader.GetString("SQCAID"),
                            SQTRANSPORTID = reader.GetString("SQTRANSPORTID"),
                            SQAMAID = reader.GetString("SQAMAID"),
                            SQAMDAID = reader.GetString("SQAMDAID"),
                            SQPRODUCT = reader.GetString("SQPRODUCT"),
                            SQUOM = reader.GetString("SQUOM"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            HSNCODE = reader.GetInt32("HSNCODE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
