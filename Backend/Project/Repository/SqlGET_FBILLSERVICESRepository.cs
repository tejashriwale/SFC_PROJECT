using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FBILLSERVICESRepository : IGET_FBILLSERVICESRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FBILLSERVICESRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FBILLSERVICES>> getasync(GET_FBILLSERVICESDomain input)
        {
            List<GET_FBILLSERVICES> result = new List<GET_FBILLSERVICES>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FBILLSERVICES]('" + input.VBKID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_FBILLSERVICES
                        {
                            SEVA = reader.GetString("SEVA"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                          
                        });
                    }
                }
            }
            return result;
        }
    }
}
