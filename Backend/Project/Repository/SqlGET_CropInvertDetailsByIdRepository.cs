using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropInvertDetailsByIdRepository : IGET_CropInvertDetailsByIdRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropInvertDetailsByIdRepository (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropInvertDetailsById>> getasync(GET_CropInvertDetailsByIdDomain input)
        {
            List<GET_CropInvertDetailsById> list = new List<GET_CropInvertDetailsById>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CropInvertDetailsById]('"+input.SHAID + "' , '"+input.COMPANYID+"' , '"+input.DEPTID+"' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertDetailsById
                        {
                         
                            SHAID = reader.GetString("SHAID"),
                            VEHNO = reader.GetString("VEHNO"),
                            PACKER = reader.GetString("PACKER"),
                            WORKER = reader.GetString("WORKER"),
                            PSIZE = reader.GetString("PSIZE"),
                            PQUNTITY = reader.GetString("PQUNTITY"),
                            WORKERNAME = reader.GetString("WORKERNAME"),
                            PACKERNAME = reader.GetString("PACKERNAME"),
                            SHDAID = reader.GetString("SHDAID"),
                            VAID = reader.GetString("VAID"),
                            DATE = reader.GetString("DATE"),
                            SIZE = reader.GetString("SIZE"),
                            COUNT = reader.GetString("COUNT"),
                            CROPINVERT = reader.GetString("CROPINVERT"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),



                        });
                    }
                }
            }
            return list;
        }
    }
}

