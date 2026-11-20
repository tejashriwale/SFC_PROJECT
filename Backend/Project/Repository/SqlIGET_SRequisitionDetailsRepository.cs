using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlIGET_SRequisitionDetailsRepository : IGET_SRequisitionDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlIGET_SRequisitionDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SRequisitionDetails>> getasync(GET_SRequisitionDetailsDomain input)
        {
           List<GET_SRequisitionDetails> list = new List<GET_SRequisitionDetails> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand ())
            {
                command.CommandText = "SELECT * FROM [GET_SRequisitionDetails](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRDAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync ())
                {
                    while(await reader.ReadAsync ())
                    {
                        list.Add(new GET_SRequisitionDetails
                        {
                            SRAID = reader.GetString("SRAID"),
                            SRDAID = reader.GetString("SRDAID"),
                            SRDPRODUCT = reader.GetString("SRDPRODUCT"),
                            SRDDESCRIPTION = reader.GetString("SRDDESCRIPTION"),
                            SRDQUANTITY = reader.GetString("SRDQUANTITY"),
                            SRDUOM = reader.GetString("SRDUOM"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
