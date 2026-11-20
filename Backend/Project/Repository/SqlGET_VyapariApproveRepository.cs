using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariApproveRepository : IGET_VyapariApproveRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariApproveRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariApprove>> getasync(GET_VyapariApproveDomain input)
        {
            List<GET_VyapariApprove> list = new List<GET_VyapariApprove>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariApprove](@p0 ,  @p1 ,@p2 , @p3)ORDER BY ISVYAPARIVERIFIED, DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariApprove
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VNAME = reader.GetString("VNAME"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BDAID = reader.GetString("BDAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                           

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariApprove>> getkeyword(GET_VyapariApproveDomain input)
        {
            List<GET_VyapariApprove> list = new List<GET_VyapariApprove>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariApprove](@p0 ,  @p1 ,@p2 , @p3)ORDER BY ISVYAPARIVERIFIED ,DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariApprove
                        {
                            BAID = reader.GetString("BAID"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VNAME = reader.GetString("VNAME"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
