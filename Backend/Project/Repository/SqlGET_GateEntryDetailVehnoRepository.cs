using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateEntryDetailVehnoRepository : IGET_GateEntryDetailVehnoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateEntryDetailVehnoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GateEntryDetailVehno>> getasync(GET_GateEntryDetailVehnoDomain input)
        {
            List<GET_GateEntryDetailVehno> result = new List<GET_GateEntryDetailVehno>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateEntryDetailVehno]('" + input.VEHNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GateEntryDetailVehno
                        {
                           
                            AADHARNO = reader.GetString("AADHARNO"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            MAID = reader.GetString("MAID"),
                            MOBILENO = reader.GetString("MOBILENO"),
                            VEHNO = reader.GetString("VEHNO"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            
                           


                        });
                    }
                }
            }
            return result;
        }
    }
}
