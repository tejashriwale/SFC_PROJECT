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
                command.CommandText = "SELECT * FROM [GET_VehicalDetailsById](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.TAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
