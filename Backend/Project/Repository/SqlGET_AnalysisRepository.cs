using System.Data;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AnalysisRepository : IGET_AnalysisRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AnalysisRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Analysis>> getasync(CounterMasterDomain masterDomain)
        {
           List<GET_Analysis> list = new List<GET_Analysis>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Analysis](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(masterDomain.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(masterDomain.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(masterDomain.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_Analysis
                        {
                            AAID = reader.GetString("AAID"),
                            QAMAID = reader.GetString("QAMAID"),
                            VAID = reader.GetString("VAID"),
                            PRAID = reader.GetString("PRAID"),
                            NETAMT = reader.GetDouble("NETAMT"),
                            ISAPPROVED = reader.GetBoolean("ISAPPROVED"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
