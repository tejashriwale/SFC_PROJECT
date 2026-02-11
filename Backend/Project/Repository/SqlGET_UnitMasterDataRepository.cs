using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_UnitMasterDataRepository : IGET_UnitMasterDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_UnitMasterDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_UnitMasterData>> GetAll()
        {
            List<GET_UnitMasterData> list = new List<GET_UnitMasterData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_UnitMasterData]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_UnitMasterData
                        {
                            UAID = reader.GetString("UAID"),
                            TOFSALE = reader.GetString("TOFSALE"),
                            QUANTITY = reader.GetDouble("QUANTITY"),
                            QUINTAL = reader.GetDouble("QUINTAL"),
                            TONNE = reader.GetDouble("TONNE"),
                            KILOGRAMS = reader.GetDouble("KILOGRAMS"),
                            GRAMS = reader.GetDouble("GRAMS"),
                            LITER = reader.GetDouble("LITER"),
                            MILILITER = reader.GetDouble("MILILITER"),
                            UNITSALE = reader.GetDouble("UNITSALE"),
                            CONVERTQTY = reader.GetDouble("CONVERTQTY"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
