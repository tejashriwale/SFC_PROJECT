using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SChallanDetailsByIdRepository : IGET_SChallanDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SChallanDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SChallanDetailsById>> getasync(GET_SChallanDetailsByIdDomain input)
        {
            List<GET_SChallanDetailsById> list = new List<GET_SChallanDetailsById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM[GET_SChallanDetailsById]('" + input.SCAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SChallanDetailsById
                        {
                            SCDAID = reader.GetString("SCDAID"),
                            SCAID = reader.GetString("SCAID"),
                            SCDPRODUCT = reader.GetString("SCDPRODUCT"),
                            SCDUOM = reader.GetString("SCDUOM"),
                            SCDQUANTITY = reader.GetString("SCDQUANTITY"),
                            SCDRATE = reader.GetDouble("SCDRATE"),
                            SCDTAMT = reader.GetDouble("SCDTAMT"),
                            SCDTAXABLE = reader.GetString("SCDTAXABLE"),
                            SCDIGST = reader.GetDouble("SCDIGST"),
                            SCDSGST = reader.GetDouble("SCDSGST"),
                            SCDCGST = reader.GetDouble("SCDCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
