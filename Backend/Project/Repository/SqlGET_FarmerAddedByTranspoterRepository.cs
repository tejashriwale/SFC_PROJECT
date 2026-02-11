using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerAddedByTranspoterRepository : IGET_FarmerAddedByTranspoterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerAddedByTranspoterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerAddedByTranspoter>> getasync(GET_FarmerAddedByTranspoterDomain input)
        {
            List<GET_FarmerAddedByTranspoter> list = new List<GET_FarmerAddedByTranspoter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerAddedByTranspoter]('" + input.REFID+"' ,'" + input.COMPANYID + "' , '"+input.DEPTID+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerAddedByTranspoter
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
