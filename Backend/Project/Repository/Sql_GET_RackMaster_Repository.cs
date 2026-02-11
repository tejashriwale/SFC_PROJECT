using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_RackMaster_Repository : I_GET_RackMaster_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_RackMaster_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_RackMaster_>> getasync(getByIdInput input)
        {
            List<_GET_RackMaster_> list = new List<_GET_RackMaster_>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from[GET_RackMaster]('%' , '%','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_RackMaster_
                        {
                            RAID = reader.GetString("RAID"),
                            RTYPE = reader.GetString("RTYPE"),
                            RDESCRIPTION = reader.GetString("RDESCRIPTION"),
                            RCODE = reader.GetString("RCODE"),
                            RROWNUMBER = reader.GetString("RROWNUMBER"),
                            RNUMBER = reader.GetString("RNUMBER"),
                            RSHELF = reader.GetString("RSHELF"),
                           
                        });
                    }
                }
            }
            return list;
         
        }

        public async Task<List<_GET_RackMaster_>> GetByGroupAsync(getByIdInput input)
        {
            List<_GET_RackMaster_> list = new List<_GET_RackMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from[GET_RackMaster]('" +input.PKID + "' ,'"+ input.KEYWORD +"','" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_RackMaster_
                        {
                            RAID = reader.GetString("RAID"),
                            RTYPE = reader.GetString("RTYPE"),
                            RDESCRIPTION = reader.GetString("RDESCRIPTION"),
                            RCODE = reader.GetString("RCODE"),
                            RROWNUMBER = reader.GetString("RROWNUMBER"),
                            RNUMBER = reader.GetString("RNUMBER"),
                            RSHELF = reader.GetString("RSHELF"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_RackMaster_>> searchasync(getByIdInput input)
        {
            List<_GET_RackMaster_> list = new List<_GET_RackMaster_>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select  * from[GET_RackMaster]('" + input.PKID + "' ,'"+ input.KEYWORD +"' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_RackMaster_
                        {

                            RAID = reader.GetString("RAID"),
                            RTYPE = reader.GetString("RTYPE"),
                            RDESCRIPTION = reader.GetString("RDESCRIPTION"),
                            RCODE = reader.GetString("RCODE"),
                            RROWNUMBER = reader.GetString("RROWNUMBER"),
                            RNUMBER = reader.GetString("RNUMBER"),
                            RSHELF = reader.GetString("RSHELF"),
                          
                        });
                    }
                }
            }
            return list;
        }
    }
}

