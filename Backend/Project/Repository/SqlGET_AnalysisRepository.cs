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
                command.CommandText = "SELECT * FROM [GET_Analysis]('" + masterDomain.PKID + "' , '" + masterDomain.COMPANYID + "' , '" + masterDomain.DEPTID + "')";
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
