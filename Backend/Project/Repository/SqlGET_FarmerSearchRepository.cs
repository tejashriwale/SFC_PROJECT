
using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerSearchRepository : IGET_FarmerSearchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerSearchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerSearch>> getasync(GET_FarmerSearchDomain input)
        {
            List<GET_FarmerSearch> list = new List<GET_FarmerSearch>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerSearch]('" + input.FAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerSearch
                        {
                            FNAME = reader.GetString("FNAME"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FCITY = reader.GetString("FCITY"),
                            FAID = reader.GetString("FAID"),
                        });
                    }

                }
               
            }
            return list;
        }
    }
}
