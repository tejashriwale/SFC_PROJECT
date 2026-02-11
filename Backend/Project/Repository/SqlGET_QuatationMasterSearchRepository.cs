using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_QuatationMasterSearchRepository : IGET_QuatationMasterSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_QuatationMasterSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_QuatationMasterSearch>> getasync(GET_QuatationMasterSearchDomain gET_Quatation)
        {
           List<GET_QuatationMasterSearch> list = new List<GET_QuatationMasterSearch>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_QuatationMasterSearch] ('" + gET_Quatation.QAMAID + "' , '" + gET_Quatation.COMPANYID + "' , '" + gET_Quatation.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_QuatationMasterSearch
                        {
                            QAMAID = reader.GetString("QAMAID"),
                            QVAID = reader.GetString("QVAID"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            QAMDAID = reader.GetString("QAMDAID"),
                            QPRODUCT = reader.GetString("QPRODUCT"),
                            QQUANTITY = reader.GetString("QQUANTITY"),
                            QUOM = reader.GetString("QUOM"),
                            QRATE = reader.GetDouble("QRATE"),
                            QTAMT = reader.GetDouble("QTAMT"),
                            QIGST = reader.GetDouble("QIGST"),
                            QSGST = reader.GetDouble("QSGST"),
                            QCGST = reader.GetDouble("QCGST"),
                            QCESS = reader.GetDouble("QCESS"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PNAME = reader.GetString("PNAME"),
                            QTRANSPORTTERMS = reader.GetString("QTRANSPORTTERMS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            HSNCODE = reader.GetInt32("HSNCODE"),



                        });
                    }
                }
                return list;
            }
        }
    }
}
