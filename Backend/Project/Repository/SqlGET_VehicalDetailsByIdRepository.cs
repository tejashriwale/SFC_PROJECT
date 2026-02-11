using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VehicalDetailsByIdRepository : IGET_VehicalDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VehicalDetailsByIdRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VehicalDetailsById>> getasync(GET_VehicalDetailsByIdDomain input)
        {
           List<GET_VehicalDetailsById> result = new List<GET_VehicalDetailsById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VehicalDetailsById]('" + input.TAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VehicalDetailsById
                        {
                            TAID = reader.GetString("TAID"),
                            VDAID = reader.GetString("VDAID"),
                            VNO = reader.GetString("VNO"),
                            VTYPE = reader.GetString("VTYPE"),
                            VCAPACITY = reader.GetString("VCAPACITY"),
                            ISDELETED = reader.GetBoolean("ISDELETED")

                        });
                    }
                }
            }
            return result;


        }
    }
}
