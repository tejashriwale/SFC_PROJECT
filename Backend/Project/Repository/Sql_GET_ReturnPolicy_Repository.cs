using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_ReturnPolicy_Repository : I_GET_ReturnPolicy_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_ReturnPolicy_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_ReturnPolicy_>> getasync(MasterInput input)
        {
            List<_GET_ReturnPolicy_> list = new List<_GET_ReturnPolicy_>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_ReturnPolicy]('%','" + input.COMPANYID + "','" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new _GET_ReturnPolicy_
                        {
                            RPAID = reader.GetString("RPAID"),
                            RPDESCRIPTION = reader.GetString("RPDESCRIPTION"),
                            RPDURATION = reader.GetString("RPDURATION"),
                            RPTERMANDCONDITION = reader.GetString("RPTERMANDCONDITION"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_ReturnPolicy_>> GetByGroupAsync(MasterInput input)
        {
            List<_GET_ReturnPolicy_> list = new List<_GET_ReturnPolicy_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT * FROM [dbo].[GET_ReturnPolicy]('" + input.PKID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "' )";

               

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_ReturnPolicy_
                        {
                            RPAID = reader.GetString("RPAID"),
                            RPDESCRIPTION = reader.GetString("RPDESCRIPTION"),
                            RPDURATION = reader.GetString("RPDURATION"),
                            RPTERMANDCONDITION = reader.GetString("RPTERMANDCONDITION"),
                          
                        });
                    }
                }
            }
            return list;
        }
    }
}
