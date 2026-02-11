using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_VYAPARIDUEDATARepository : IGET_VYAPARIDUEDATARepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VYAPARIDUEDATARepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VYAPARIDUEDATA>> getasync(GET_VYAPARIDUEDATADomain input)
        {
            List<GET_VYAPARIDUEDATA> result = new List<GET_VYAPARIDUEDATA>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VYAPARIDUEDATA]('" + input.VDAID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VYAPARIDUEDATA
                        {
                            vdaid = reader.GetString("vdaid"),
                            TRNDT = reader.GetString("TRNDT"),
                            TAMT = reader.GetDouble("TAMT"),
                            BPAMT = reader.GetDouble("BPAMT"),
                            FDUEDT = reader.GetString("FDUEDT"),
                            SDUEDT = reader.GetString("SDUEDT"),
                            VPAID = reader.GetString("VPAID"),
                            TID = reader.GetString("TID"),
                            TRNTID = reader.GetString("TRNTID"),
                            TRNSR = reader.GetString("TRNSR"),
                            REFKEY = reader.GetString("REFKEY"),
                           
                        });
                    }
                }
            }
            return result;
        }
    }
}
