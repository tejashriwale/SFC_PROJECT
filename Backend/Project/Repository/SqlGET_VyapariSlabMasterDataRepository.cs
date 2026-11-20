using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariSlabMasterDataRepository : IGET_VyapariSlabMasterDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariSlabMasterDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariSlabMasterData>> getasync(GET_VyapariSlabMasterDataDomain input)
        {
            List<GET_VyapariSlabMasterData> list = new List<GET_VyapariSlabMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariSlabMasterData]( @p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.VSAID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariSlabMasterData
                        {
                            VSAID	 = reader.GetString("VSAID"),
                            SCHEMENO = reader.GetString("SCHEMENO"),
                            SCHEMEDATE = reader.GetString("SCHEMEDATE"),
                            SCHEMENAME	 = reader.GetString("SCHEMENAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            STATUS		 = reader.GetString("STATUS"),
                            VSDAID     = reader.GetString("VSDAID"),
                            STARTDATE	 = reader.GetString("STARTDATE"),
                            ENDDATE	 = reader.GetString("ENDDATE"),
                            DUEDATE	 = reader.GetString("DUEDATE"),
                            KALAVADHI	 = reader.GetString("KALAVADHI"),
                            PERCENTS = reader.GetString("PERCENTS"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
