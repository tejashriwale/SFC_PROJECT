using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DistrictRepository : IGET_DistrictRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DistrictRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_District>> GetAll()
        {
            List<GET_District> list = new List<GET_District>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_District]()";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_District
                        {
                            SDISTRICT = reader.GetString("SDISTRICT")
                        });

                    }
                }
            }
            return list;


        }
    }
}
