using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SQuatationDetailsRepository : IGET_SQuatationDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SQuatationDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SQuatationDetails>> getasync(GET_SQuatationDetailsDomain input)
        {
            List<GET_SQuatationDetails> list = new List<GET_SQuatationDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SQuatationDetails](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SQAMDAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new GET_SQuatationDetails
                        {
                            SQAMAID = reader.GetString("SQAMAID"),
                            SQAMDAID = reader.GetString("SQAMDAID"),
                            SQPRODUCT = reader.GetString("SQPRODUCT"),
                            SQQUANTITY = reader.GetString("SQQUANTITY"),
                            SQUOM = reader.GetString("SQUOM"),
                            SQRATE = reader.GetDouble("SQRATE"),
                            SQTAMT = reader.GetDouble("SQTAMT"),
                            SQIGST = reader.GetDouble("SQIGST"),
                            SQSGST = reader.GetDouble("SQSGST"),
                            SQCGST = reader.GetDouble("SQCGST"),
                            SQCESS = reader.GetDouble("SQCESS"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            SQTAXBALEAMOUNT = reader.GetInt32("SQTAXBALEAMOUNT"),
                            IsDeleted = reader.GetBoolean("IsDeleted"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
