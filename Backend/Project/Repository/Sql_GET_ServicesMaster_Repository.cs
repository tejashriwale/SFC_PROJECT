using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_ServicesMaster_Repository : I_GET_ServicesMaster_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_ServicesMaster_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_ServicesMaster_>> getasync(getByIdInput input)
        {
            List<_GET_ServicesMaster_> list = new List<_GET_ServicesMaster_>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_ServicesMaster]('%' , '%',@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_ServicesMaster_
                        {
                            SAID = reader.GetString("SAID"),
                            SCODE = reader.GetString("SCODE"),
                            SNAME = reader.GetString("SNAME"),
                            STYPE = reader.GetString("STYPE"),
                            SDESCRIPTION = reader.GetString("SDESCRIPTION"),
                            SCATEGORY = reader.GetString("SCATEGORY"),
                            SSUBCATEGORY = reader.GetString("SSUBCATEGORY"),
                            SHSN = reader.GetString("SHSN"),
                            CTNAME = reader.GetString("CTNAME"),
                            SCTNAME = reader.GetString("SCTNAME"),
                            SRNAME = reader.GetString("SRNAME"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_ServicesMaster_>> Get_GET_ServicesAsync(getByIdInput input)
        {
            List<_GET_ServicesMaster_> list = new List<_GET_ServicesMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 * from [GET_ServicesMaster](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }



                await dbContext.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_ServicesMaster_
                        {
                            SAID = reader.GetString("SAID"),
                            SCODE = reader.GetString("SCODE"),
                            SNAME = reader.GetString("SNAME"),
                            STYPE = reader.GetString("STYPE"),
                            SDESCRIPTION = reader.GetString("SDESCRIPTION"),
                            SCATEGORY = reader.GetString("SCATEGORY"),
                            SSUBCATEGORY = reader.GetString("SSUBCATEGORY"),
                            SHSN = reader.GetString("SHSN"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_ServicesMaster_>> searchasync(getByIdInput input)
        {
            List<_GET_ServicesMaster_> list = new List<_GET_ServicesMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select  * from [GET_ServicesMaster](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }

                dbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_ServicesMaster_
                        {
                            SAID = reader.GetString("SAID"),
                            SCODE = reader.GetString("SCODE"),
                            SNAME = reader.GetString("SNAME"),
                            STYPE = reader.GetString("STYPE"),
                            SDESCRIPTION = reader.GetString("SDESCRIPTION"),
                            SCATEGORY = reader.GetString("SCATEGORY"),
                            SSUBCATEGORY = reader.GetString("SSUBCATEGORY"),
                            SHSN = reader.GetString("SHSN"),
                            
                        });
                    }
                }
                return list;
            }
        }
    }
}
