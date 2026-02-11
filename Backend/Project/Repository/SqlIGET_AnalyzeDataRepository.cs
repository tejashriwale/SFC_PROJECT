using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlIGET_AnalyzeDataRepository : IGET_AnalyzeDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlIGET_AnalyzeDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AnalyzeData>> getasync(GET_AnalyzeDataDomain gET_Analyze)
        {
            List<GET_AnalyzeData> list = new List<GET_AnalyzeData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AnalyzeData]('" + gET_Analyze.PRAID + "' , '" + gET_Analyze.COMPANYID + "' , '" + gET_Analyze.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_AnalyzeData
                        {
                            QAMAID = reader.GetString("QAMAID"),
                            QVAID = reader.GetString("QVAID"),
                            QVCONTACT = reader.GetString("QVCONTACT"),
                            QVEMAIL = reader.GetString("QVEMAIL"),
                            QVADDRESS = reader.GetString("QVADDRESS"),
                            PRNO = reader.GetString("PRNO"),
                            QMNAMT = reader.GetDouble("QMNAMT"),
                            QGAMT = reader.GetDouble("QGAMT"),
                            QNO = reader.GetString("QNO"),
                            QDATE = reader.GetString("QDATE"),
                            QDUEDATE = reader.GetString("QDUEDATE"),
                            QTRANSPORTTERMS = reader.GetString("QTRANSPORTTERMS"),
                            QPAYMENTTERMS = reader.GetString("QPAYMENTTERMS"),
                            ISAPPROVED = reader.GetBoolean("ISAPPROVED"),
                            QPDUEDATE = reader.GetString("QPDUEDATE"),
                            QNODAYS = reader.GetInt32("QNODAYS"),
                            VENDORNAME = reader.GetString("VENDORNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
