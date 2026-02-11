using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PlotMasterRepository : IGET_PlotMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PlotMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PlotMaster>> getasync(GET_PlotMasterDomain input)
        {
            List<GET_PlotMaster> list = new List<GET_PlotMaster>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PlotMaster]('" + input.PLTAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY REGDATE DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_PlotMaster
                        {
                           
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            PLTAID = reader.GetString("PLTAID"),
                            REGDATE = reader.GetString("REGDATE"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PlotMaster>> getkeyword(GET_PlotMasterDomain input)
        {
            List<GET_PlotMaster> list = new List<GET_PlotMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PlotMaster]('" + input.PLTAID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY REGDATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PlotMaster
                        {
                            
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            PLTAID = reader.GetString("PLTAID"),
                            REGDATE = reader.GetString("REGDATE"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
