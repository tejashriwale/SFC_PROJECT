using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariDueRepository : IGET_VyapariDueRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariDueRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariDue>> getasync(GET_VyapariDueDomain input)
        {
            List<GET_VyapariDue> list = new List<GET_VyapariDue>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariDue](@p0 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VDAID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())

                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariDue
                        {
                            VDAID = reader.GetString("VDAID"),
                            TRNDT = reader.GetString("TRNDT"),
                            TAMT = reader.GetDouble("TAMT"),
                            BPAMT = reader.GetDouble("BPAMT"),
                            FDUEDT = reader.GetString("FDUEDT"),
                            SDUEDT = reader.GetString("SDUEDT"),
                            VPAID = reader.GetString("VPAID"),
                            TID = reader.GetString("TID"),
                            STID = reader.GetString("STID"),
                            TRNTID = reader.GetString("TRNTID"),
                            TRNSR = reader.GetString("TRNSR"),
                            REFKEY = reader.GetString("REFKEY"),
                            UAID = reader.GetString("UAID"),
                            VREFF = reader.GetString("VREFF"),
                            REFF = reader.GetString("REFF"),
                            VTYPE = reader.GetString("VTYPE"),
                            VNAME = reader.GetString("VNAME"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE"),

                        });
                    }
            }
            return list;
        } 
            
          
        
    }
}
