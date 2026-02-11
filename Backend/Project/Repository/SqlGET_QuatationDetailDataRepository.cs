using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_QuatationDetailDataRepository : IGET_QuatationDetailDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_QuatationDetailDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_QuatationDetailData>> getasync(GET_QuatationDetailDataDomain input)
        {
            List<GET_QuatationDetailData> list = new List<GET_QuatationDetailData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_QuatationDetailData]('" + input.QAMAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_QuatationDetailData
                        {
                            QAMDAID = reader.GetString("QAMDAID"),
                            QAMAID = reader.GetString("QAMAID"),
                            QPRODUCT = reader.GetString("QPRODUCT"),
                            QUOM = reader.GetString("QUOM"),
                            QQUANTITY = reader.GetString("QQUANTITY"),
                            QRATE = reader.GetDouble("QRATE"),
                            QTAMT = reader.GetDouble("QTAMT"),
                            QIGST = reader.GetDouble("QIGST"),
                            QSGST = reader.GetDouble("QSGST"),
                            QCGST = reader.GetDouble("QCGST"),
                            QCESS = reader.GetDouble("QCESS"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            HSNCODE = reader.GetInt32("HSNCODE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
