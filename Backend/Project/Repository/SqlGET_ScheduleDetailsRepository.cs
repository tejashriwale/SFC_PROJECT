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
                command.CommandText = "SELECT * FROM [GET_ScheduleDetails] (@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
