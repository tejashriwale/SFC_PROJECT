using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateFarmerRepository : IGET_GateFarmerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateFarmerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateFarmer>> getasync(GET_GateFarmerDomain input)
        {
            List<GET_GateFarmer> list = new List<GET_GateFarmer> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateFarmer](@p0 , @p1 , @p2 , @p3)ORDER BY date DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.FAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_GateFarmer
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FBIRTHDATE = reader.GetString("FBIRTHDATE"),
                            FPANNO = reader.GetString("FPANNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            FBRANCHNAME = reader.GetString("FBRANCHNAME"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            FPINCODE = reader.GetString("FPINCODE"),
                            FSTATE = reader.GetString("FSTATE"),
                            FCITY = reader.GetString("FCITY"),
                            FCROPAREA = reader.GetString("FCROPAREA"),
                            FCROPTYPE = reader.GetString("FCROPTYPE"),
                            FSTATUS = reader.GetString("FSTATUS"),
                            FAddress = reader.GetString("FAddress"),
                            UID = reader.GetString("UID"),
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            REFID = reader.GetString("REFID"),
                            FPROFILE = reader.GetString("FPROFILE"),
                            DATE = reader.GetString("DATE"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_GateFarmer>> getkeyword(GET_GateFarmerDomain input)
        {
            List<GET_GateFarmer> list = new List<GET_GateFarmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateFarmer](@p0 , @p1 , @p2 , @p3)ORDER BY date DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.FAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateFarmer
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FBIRTHDATE = reader.GetString("FBIRTHDATE"),
                            FPANNO = reader.GetString("FPANNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            FBRANCHNAME = reader.GetString("FBRANCHNAME"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            FPINCODE = reader.GetString("FPINCODE"),
                            FSTATE = reader.GetString("FSTATE"),
                            FCITY = reader.GetString("FCITY"),
                            FCROPAREA = reader.GetString("FCROPAREA"),
                            FCROPTYPE = reader.GetString("FCROPTYPE"),
                            FSTATUS = reader.GetString("FSTATUS"),
                            FAddress = reader.GetString("FAddress"),
                            UID = reader.GetString("UID"),
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            REFID = reader.GetString("REFID"),
                            FPROFILE = reader.GetString("FPROFILE"),
                            DATE = reader.GetString("DATE"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
