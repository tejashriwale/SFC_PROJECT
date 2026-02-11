using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_ScheduleDetailsRepository : IGET_ScheduleDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ScheduleDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ScheduleDetails>> getasync(GET_ScheduleDetailsDomain input)
        {
            List<GET_ScheduleDetails> list = new List<GET_ScheduleDetails>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ScheduleDetails] ('" + input.SHAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ScheduleDetails
                        {
                            SHAID = reader.GetString("SHAID"),
                            SHDAID = reader.GetString("SHDAID"),
                            SHSDATE = reader.GetString("SHSDATE"),
                            PACKER = reader.GetString("PACKER"),
                            PQUNTITY = reader.GetString("PQUNTITY"),
                            PSIZE = reader.GetString("PSIZE"),
                            VEHNO = reader.GetString("VEHNO"),
                            WORKER = reader.GetString("WORKER"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
