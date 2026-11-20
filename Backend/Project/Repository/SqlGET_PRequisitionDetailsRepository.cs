using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PRequisitionDetailsRepository : IGET_PRequisitionDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_PRequisitionDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PRequisitionDetails>> getasync(RepuisitonDomain input)
        {
           List<GET_PRequisitionDetails> list = new List<GET_PRequisitionDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PRequisitionDetails](@p0,@p1,@p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_PRequisitionDetails
                        {
                            PRDAID = reader.GetString("PRDAID"),
                            PRAID = reader.GetString("PRAID"),
                            PRDPRODUCT = reader.GetString("PRDPRODUCT"),
                            PRDDESCRIPTION = reader.GetString("PRDDESCRIPTION"),
                            PRDQUANTITY = reader.GetString("PRDQUANTITY"),
                            PRDUOM = reader.GetString("PRDUOM"),
                            PNAME = reader.GetString("PNAME"),
                            PIGST = reader.GetDouble("PIGST"),
                            PSGST = reader.GetDouble("PSGST"),
                            PCGST = reader.GetDouble("PCGST"),
                            PCESS = reader.GetDouble("PCESS"),
                            UOMTITLE = reader.GetString("UOMTITLE"),
                            RATE = reader.GetDouble("RATE"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            PRBASEQTY = reader.GetInt32("PRBASEQTY"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
