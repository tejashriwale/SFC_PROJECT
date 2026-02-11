using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FarmerSearchBYAadhaarRepository : IGET_FarmerSearchBYAadhaarRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerSearchBYAadhaarRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerSearchBYAadhaar>> getasync(GET_FarmerSearchBYAadhaarDomain input)
        {
            List<GET_FarmerSearchBYAadhaar> list = new List<GET_FarmerSearchBYAadhaar> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerSearchBYAadhaar]('" + input.FADDHARNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerSearchBYAadhaar
                        {
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FNAME = reader.GetString("FNAME"),
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
