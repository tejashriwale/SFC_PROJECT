using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ComplaintRepository : IGET_ComplaintRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ComplaintRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Complaint>> getasync(GET_ComplaintDomain input)
        {
            List<GET_Complaint> list = new List<GET_Complaint>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Complaint](@p0 ,  @p1   ,@p2 , @p3)ORDER BY date DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Complaint
                        {
                            PKID = reader.GetString("PKID"),
                            NAME = reader.GetString("NAME"),
                            DATE = reader.GetString("DATE"),
                            UAID = reader.GetString("UAID"),
                            TITLE = reader.GetString("TITLE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            STATUS = reader.GetString("STATUS"),
                            CONTACT = reader.GetString("CONTACT"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Complaint>> getkeyword(GET_ComplaintDomain input)
        {
            List<GET_Complaint> list = new List<GET_Complaint>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Complaint](@p0 ,  @p1   ,@p2 , @p3)ORDER BY date DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Complaint
                        {
                            PKID = reader.GetString("PKID"),
                            NAME = reader.GetString("NAME"),
                            DATE = reader.GetString("DATE"),
                            UAID = reader.GetString("UAID"),
                            TITLE = reader.GetString("TITLE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            STATUS = reader.GetString("STATUS"),
                            CONTACT = reader.GetString("CONTACT"),
                            UFORENAME = reader.GetString("UFORENAME"),
                            UMOBILENUMBER = reader.GetString("UMOBILENUMBER"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
