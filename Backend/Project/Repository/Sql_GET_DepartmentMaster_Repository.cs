using System.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.Repository
{
    public class Sql_GET_DepartmentMaster_Repository : I_GET_DepartmentMaster_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_DepartmentMaster_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_DepartmentMaster_>> getasync(DepartmentMasterDomain  input)
        {
           List<_GET_DepartmentMaster_> list = new List<_GET_DepartmentMaster_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_DepartmentMaster]('%' , '%' ,'"+ input.COMPANYID +"')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new _GET_DepartmentMaster_
                        {
                            DEPTAID = reader.GetString("DEPTAID"),
                            WAID = reader.GetString("WAID"),
                            DNAME = reader.GetString("DNAME"),
                            STOREID=reader.GetString("STOREID"),
                            DINTERCOMEXTENSION = reader.GetString("DINTERCOMEXTENSION"),
                            DCODE = reader.GetString("DCODE"),
                            DADDRESS = reader.GetString("DADDRESS"),
                            DCITY = reader.GetString("DCITY"),
                            DAREA = reader.GetString("DAREA"),
                            DSTATE = reader.GetString("DSTATE"),
                            DLANDMARK = reader.GetString("DLANDMARK"),
                            DPINCODE = reader.GetString("DPINCODE"),
                            WNAME = reader.GetString("WNAME"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME")

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_DepartmentMaster_>> GetByGroupAsync(DepartmentMasterDomain input)
        {
            List<_GET_DepartmentMaster_> list = new List<_GET_DepartmentMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 *from [GET_DepartmentMaster]('" + input.DEPTAID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "')";

                

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_DepartmentMaster_
                        {
                            DEPTAID = reader.GetString("DEPTAID"),
                            WAID = reader.GetString("WAID"),
                            DNAME = reader.GetString("DNAME"),
                            STOREID = reader.GetString("STOREID"),
                            DINTERCOMEXTENSION = reader.GetString("DINTERCOMEXTENSION"),
                            DCODE = reader.GetString("DCODE"),
                            DADDRESS = reader.GetString("DADDRESS"),
                            DCITY = reader.GetString("DCITY"),
                            DAREA = reader.GetString("DAREA"),
                            DSTATE = reader.GetString("DSTATE"),
                            DLANDMARK = reader.GetString("DLANDMARK"),
                            DPINCODE = reader.GetString("DPINCODE"),
                            WNAME = reader.GetString("WNAME"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME")

                        });
                    }
                }
                return list;
            }
        }

        public async Task<List<_GET_DepartmentMaster_>> searchasync(DepartmentMasterDomain input)
        {
            List<_GET_DepartmentMaster_> list = new List<_GET_DepartmentMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select  * from [GET_DepartmentMaster]('" + input.DEPTAID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "')";

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_DepartmentMaster_
                        {
                            DEPTAID = reader.GetString("DEPTAID"),
                            WAID = reader.GetString("WAID"),
                            DNAME = reader.GetString("DNAME"),
                            STOREID = reader.GetString("STOREID"),
                            DINTERCOMEXTENSION = reader.GetString("DINTERCOMEXTENSION"),
                            DCODE = reader.GetString("DCODE"),
                            DADDRESS = reader.GetString("DADDRESS"),
                            DCITY = reader.GetString("DCITY"),
                            DAREA = reader.GetString("DAREA"),
                            DSTATE = reader.GetString("DSTATE"),
                            DLANDMARK = reader.GetString("DLANDMARK"),
                            DPINCODE = reader.GetString("DPINCODE"),
                            WNAME = reader.GetString("WNAME"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME")

                        });
                    }
                }
            }
            return list;
        }
    }
}
