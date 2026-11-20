using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SchedulerMasterRepository : IGET_SchedulerMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SchedulerMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SchedulerMaster>> getasync(GET_SchedulerMasterDomain input)
        {
            List<GET_SchedulerMaster> list = new List<GET_SchedulerMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SchedulerMaster](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SchedulerMaster
                        {
                           VAID = reader.GetString("VAID"),
                           VISITINGDATE = reader.GetString("VISITINGDATE"),
                           CROPAREA = reader.GetString("CROPAREA"),
                           EXPECTEDHARVESTINGDATE = reader.GetString("EXPECTEDHARVESTINGDATE"),
                           CROPTYPE = reader.GetString("CROPTYPE"),
                           CROPVARIETY = reader.GetString("CROPVARIETY"),
                           FARMERNAME = reader.GetString("FARMERNAME"),
                           LOCATION = reader.GetString("LOCATION"),
                           EXPECTEDPRODUCTION = reader.GetString("EXPECTEDPRODUCTION"),
                           PHONENUMBER = reader.GetString("PHONENUMBER"),
                           


                        });
                    }
                }
            }
            return list;
        }
    }
}
