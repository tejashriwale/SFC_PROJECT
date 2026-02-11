using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_CounterMaster_Repository : I_GET_CounterMaster_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_CounterMaster_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_CounterMaster_>> getasync(MasterInput input)
        {
           List<_GET_CounterMaster_> list = new List<_GET_CounterMaster_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_CounterMaster]('%','" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new _GET_CounterMaster_
                        {
                            CAID = reader.GetString("CAID"),
                            CNAME = reader.GetString("CNAME"),
                            CDATE = reader.GetString("CDATE"),
                            CINCHARGE = reader.GetString("CINCHARGE"),
                            CINTERCOM = reader.GetString("CINTERCOM"),
                            CCODE = reader.GetString("CCODE"),
                            DAID = reader.GetString("DAID"),
                            WAID = reader.GetString("WAID"),
                            EMPNAME = reader.GetString("EMPNAME"),
                            DNAME = reader.GetString("DNAME"),
                            WNAME = reader.GetString("WNAME"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_CounterMaster_>> GetByGroupAsync(MasterInput input)
        {
            List<_GET_CounterMaster_> list = new List<_GET_CounterMaster_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT * FROM [dbo].[GET_CounterMaster]('"+input.PKID+"' ,'"+ input.COMPANYID +"' , '"+ input.DEPTID +"')";

            

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {

                        list.Add(new _GET_CounterMaster_
                        {
                            CAID = reader.GetString("CAID"),
                            CNAME = reader.GetString("CNAME"),
                            CINCHARGE = reader.GetString("CINCHARGE"),
                            CINTERCOM = reader.GetString("CINTERCOM"),
                            CCODE = reader.GetString("CCODE"),
                            DAID = reader.GetString("DAID"),
                            WAID = reader.GetString("WAID"),
                            
                        });
                    }
                }

               

            }
            return (list);
           
        }
    }
}
