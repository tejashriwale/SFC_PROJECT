using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SChallanDetailsRepository : IGET_SChallanDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SChallanDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SChallanDetails>> getasync(GET_SChallanDetailsDomain input)
        {
           List<GET_SChallanDetails> list = new List<GET_SChallanDetails> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_SChallanDetails] ('" + input.SCAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SChallanDetails
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

                        });
                    }
                }
            }
            return list;
        }
    }
}
