using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TransportersRepository : IGET_TransportersRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TransportersRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Transporters>> getasync(GET_TransportersDomain input)
        {
            List<GET_Transporters> list = new List<GET_Transporters>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Transporters](@p0 , @p1 , @p2 , @p3)ORDER BY TDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.TAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_Transporters
                        {
                            TAID = reader.GetString("TAID"),
                            TNAME = reader.GetString("TNAME"),
                            VEHICALNO = reader.GetString("VEHICALNO"),
                            TPAN = reader.GetString("TPAN"),
                            VEHICALTYPE = reader.GetString("VEHICALTYPE"),
                            VEHICALCAPACITY = reader.GetString("VEHICALCAPACITY"),
                            TBIRTHDATE = reader.GetString("TBIRTHDATE"),
                            TGSTIN	 = reader.GetString("TGSTIN"),
                            TTSTATE = reader.GetString("TTSTATE"),
                            TADDRESS = reader.GetString("TADDRESS"),
                            TCITY = reader.GetString("TCITY"),
                            TPINCODE = reader.GetString("TPINCODE"),
                            TCANTACTNO = reader.GetString("TCANTACTNO"),
                            TDATE = reader.GetString("TDATE"),
                            TPROFILE = reader.GetString("TPROFILE"),
                            TUID = reader.GetString("TUID"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Transporters>> getkeyword(GET_TransportersDomain input)
        {
            List<GET_Transporters> list = new List<GET_Transporters>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Transporters](@p0 , @p1 , @p2 , @p3)ORDER BY TDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.TAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Transporters
                        {
                            TAID = reader.GetString("TAID"),
                            TNAME = reader.GetString("TNAME"),
                            VEHICALNO = reader.GetString("VEHICALNO"),
                            TPAN = reader.GetString("TPAN"),
                            VEHICALTYPE = reader.GetString("VEHICALTYPE"),
                            VEHICALCAPACITY = reader.GetString("VEHICALCAPACITY"),
                            TBIRTHDATE = reader.GetString("TBIRTHDATE"),
                            TGSTIN = reader.GetString("TGSTIN"),
                            TTSTATE = reader.GetString("TTSTATE"),
                            TADDRESS = reader.GetString("TADDRESS"),
                            TCITY = reader.GetString("TCITY"),
                            TPINCODE = reader.GetString("TPINCODE"),
                            TCANTACTNO = reader.GetString("TCANTACTNO"),
                            TDATE = reader.GetString("TDATE"),
                            TPROFILE = reader.GetString("TPROFILE"),
                            TUID = reader.GetString("TUID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
