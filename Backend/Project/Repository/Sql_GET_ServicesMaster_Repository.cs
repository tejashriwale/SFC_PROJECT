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
                command.CommandText = "select Top 20 * from [GET_ServicesMaster]('%' , '%','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "select Top 20 * from [GET_ServicesMaster]('" + input.PKID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";



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
                command.CommandText = "select  * from [GET_ServicesMaster]('" + input.PKID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";

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
