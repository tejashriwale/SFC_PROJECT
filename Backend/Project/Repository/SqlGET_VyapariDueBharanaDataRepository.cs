using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariDueBharanaDataRepository : IGET_VyapariDueBharanaDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariDueBharanaDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariDueBharanaData>> getasync(GET_VyapariDueBharanaDataDomain input)
        {
            List<GET_VyapariDueBharanaData> result = new List<GET_VyapariDueBharanaData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariDueBharanaData]('" + input.VPAID + "' , '" + input.TRNDT + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariDueBharanaData
                        {
                            VPAID = reader.GetString("VPAID"),
                            BPAMT = reader.GetDouble("BPAMT"),
                            TRNDT = reader.GetString("TRNDT"),
                            FDUEDT = reader.GetString("FDUEDT"),
                            VAPASI = reader.GetDouble("VAPASI"),
                            VAMT = reader.GetDouble("VAMT"),
                           
                        });
                    }
                }
            }
            return result;
        }
    }
}
