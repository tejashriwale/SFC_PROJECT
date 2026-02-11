using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_FarmerAddedByTranspoterAllRepository : IGET_FarmerAddedByTranspoterAllRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerAddedByTranspoterAllRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerAddedByTranspoterAll>> getasync(GET_FarmerAddedByTranspoterAllDomain input)
        {
            List<GET_FarmerAddedByTranspoterAll> list = new List<GET_FarmerAddedByTranspoterAll>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM[GET_FarmerAddedByTranspoterAll]('" + input.REFID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerAddedByTranspoterAll
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
