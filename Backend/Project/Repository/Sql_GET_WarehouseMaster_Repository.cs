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
                command.CommandText = "select Top 20 * from [GET_WarehouseMaster]('%' , '%','" + input.COMPANYID + "' , '" + input.DEPTID + "')";

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
                command.CommandText = "select Top 20 *from [GET_WarehouseMaster]('" + input.PKID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";

               
            

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
                command.CommandText = "select  * from [GET_WarehouseMaster]('" + input.PKID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";

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
