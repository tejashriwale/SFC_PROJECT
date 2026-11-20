using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SReturnDetailsRepository : IGET_SReturnDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SReturnDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_SReturnDetails>> getasync(GET_SReturnDetailsDomain input)
        {
            List<GET_SReturnDetails> list = new List<GET_SReturnDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SReturnDetails](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRDAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_SReturnDetails
                        {
                            SRAID = reader.GetString("SRAID"),
                            SRDAID = reader.GetString("SRDAID"),
                            SRDPRODUCT = reader.GetString("SRDPRODUCT"),
                            SRDUOM = reader.GetString("SRDUOM"),
                            SRDQUANTITY = reader.GetString("SRDQUANTITY"),
                            SRTDAMT = reader.GetDouble("SRTDAMT"),
                            SRDIGST = reader.GetDouble("SRDIGST"),
                            SRDSGST = reader.GetDouble("SRDSGST"),
                            SRDCGST = reader.GetDouble("SRDCGST"),
                            SRDQUANTITYRETURN = reader.GetString("SRDQUANTITYRETURN"),
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
