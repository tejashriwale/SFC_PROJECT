using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariSlabMasterRepository : IGET_VyapariSlabMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariSlabMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariSlabMaster>> getasync(GET_VyapariSlabMasterDomain input)
        {
            List<GET_VyapariSlabMaster> list = new List<GET_VyapariSlabMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariSlabMaster]( '" + input.VSAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariSlabMaster
                        {
                            VSAID		 = reader.GetString("VSAID"),
                            SCHEMENO	 = reader.GetString("SCHEMENO"),
                            SCHEMEDATE	 = reader.GetString("SCHEMEDATE"),
                            SCHEMENAME	 = reader.GetString("SCHEMENAME"),
                            DESCRIPTION= reader.GetString("DESCRIPTION"),
                            STATUS = reader.GetString("STATUS")
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariSlabMaster>> getkeyword(GET_VyapariSlabMasterDomain input)
        {
            List<GET_VyapariSlabMaster> list = new List<GET_VyapariSlabMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariSlabMaster]( '" + input.VSAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariSlabMaster
                        {
                            VSAID = reader.GetString("VSAID"),
                            SCHEMENO = reader.GetString("SCHEMENO"),
                            SCHEMEDATE = reader.GetString("SCHEMEDATE"),
                            SCHEMENAME = reader.GetString("SCHEMENAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            STATUS = reader.GetString("STATUS")
                        });
                    }
                }
            }
            return list;
        }
    }
}
