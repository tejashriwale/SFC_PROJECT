using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_WarehouseMaster_Repository : I_GET_WarehouseMaster_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_WarehouseMaster_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_WarehouseMaster_>> getasync(getByIdInput input)
        {
            List<_GET_WarehouseMaster_> list = new List<_GET_WarehouseMaster_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_WarehouseMaster]('%' , '%',@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new _GET_WarehouseMaster_
                        {
                            WAID = reader.GetString("WAID"),
                            WNAME = reader.GetString("WNAME"),
                            WAVAILABLECAPCITY = reader.GetString("WAVAILABLECAPCITY"),
                            WINTERCOMEXTENSION = reader.GetString("WINTERCOMEXTENSION"),
                            WADDRESS = reader.GetString("WADDRESS"),
                            WCITY = reader.GetString("WCITY"),
                            WAREA = reader.GetString("WAREA"),
                            WSTOREID=reader.GetString("WSTOREID"),
                            WSTATE = reader.GetString("WSTATE"),
                            WLANDMARK = reader.GetString("WLANDMARK"),
                            WPINCODE = reader.GetString("WPINCODE"),
                            WDESCRIPTION = reader.GetString("WDESCRIPTION"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_WarehouseMaster_>> GetByGroupAsync(getByIdInput input)
        {
            List<_GET_WarehouseMaster_> list = new List<_GET_WarehouseMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 *from [GET_WarehouseMaster](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }

               
            

                dbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_WarehouseMaster_
                        {
                            WAID = reader.GetString("WAID"),
                            WNAME = reader.GetString("WNAME"),
                            WAVAILABLECAPCITY = reader.GetString("WAVAILABLECAPCITY"),
                            WINTERCOMEXTENSION = reader.GetString("WINTERCOMEXTENSION"),
                            WADDRESS = reader.GetString("WADDRESS"),
                            WSTOREID = reader.GetString("WSTOREID"),
                            WCITY = reader.GetString("WCITY"),
                            WAREA = reader.GetString("WAREA"),
                            WSTATE = reader.GetString("WSTATE"),
                            WLANDMARK = reader.GetString("WLANDMARK"),
                            WPINCODE = reader.GetString("WPINCODE"),
                            WDESCRIPTION = reader.GetString("WDESCRIPTION"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            SDISTRICT = reader.GetString("SDISTRICT"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_WarehouseMaster_>> searchasync(getByIdInput input)
        {
            List<_GET_WarehouseMaster_> list = new List<_GET_WarehouseMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select  * from [GET_WarehouseMaster](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_WarehouseMaster_
                        {
                            WAID = reader.GetString("WAID"),
                            WNAME = reader.GetString("WNAME"),
                            WAVAILABLECAPCITY = reader.GetString("WAVAILABLECAPCITY"),
                            WINTERCOMEXTENSION = reader.GetString("WINTERCOMEXTENSION"),
                            WADDRESS = reader.GetString("WADDRESS"),
                            WSTOREID = reader.GetString("WSTOREID"),
                            WCITY = reader.GetString("WCITY"),
                            WAREA = reader.GetString("WAREA"),
                            WSTATE = reader.GetString("WSTATE"),
                            WLANDMARK = reader.GetString("WLANDMARK"),
                            WPINCODE = reader.GetString("WPINCODE"),
                            WDESCRIPTION = reader.GetString("WDESCRIPTION"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            SDISTRICT = reader.GetString("SDISTRICT"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
