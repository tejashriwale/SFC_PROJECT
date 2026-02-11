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
                command.CommandText = "SELECT * FROM [GET_VyapariSlabMasterData]( '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.VSAID + "')";
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
