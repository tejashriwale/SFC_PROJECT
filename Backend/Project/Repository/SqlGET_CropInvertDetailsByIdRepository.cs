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
                command.CommandText = "SELECT * FROM [GET_CropInvertDetailsById](@p0 , @p1 , @p2 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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

