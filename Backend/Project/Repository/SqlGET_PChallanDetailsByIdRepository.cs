using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PChallanDetailsByIdRepository : IGET_PChallanDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PChallanDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PChallanDetailsById>> getasync(GET_PChallanDetailsByIdDomain input)
        {
            List<GET_PChallanDetailsById> list = new List<GET_PChallanDetailsById>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PChallanDetailsById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PCAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PChallanDetailsById
                        {
                            PCAID = reader.GetString("PCAID"),
                            PCDAID = reader.GetString("PCDAID"),
                            PCDPRODUCT = reader.GetString("PCDPRODUCT"),
                            PCDUOM = reader.GetString("PCDUOM"),
                            PCDQUANTITY = reader.GetString("PCDQUANTITY"),
                            PCDRATE = reader.GetDouble("PCDRATE"),
                            PCDTAMT = reader.GetDouble("PCDTAMT"),
                            PCDTAXABLE = reader.GetString("PCDTAXABLE"),
                            PCDIGST = reader.GetDouble("PCDIGST"),
                            PCDSGST = reader.GetDouble("PCDSGST"),
                            PCDCGST = reader.GetDouble("PCDCGST"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            BATCHNO = reader.GetString("BATCHNO"),
                            EXPIRYDATE = reader.GetString("EXPIRYDATE"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            PRODUCTNAME = reader.GetString("PRODUCTNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
