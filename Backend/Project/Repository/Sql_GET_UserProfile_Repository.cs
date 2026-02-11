using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_UserProfile_Repository : I_GET_UserProfile_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_UserProfile_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_UserProfile_>> getasync(getByIdInput input)
        {
           List<_GET_UserProfile_> list = new List<_GET_UserProfile_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_UserProfile]('%')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_UserProfile_
                        {
                            UserAutoID = reader.GetString("UserAutoID"),
                            Username = reader.GetString("Username"),
                            Mobile = reader.GetString("Mobile"),
                            Password = reader.GetString("Password"),
                            Registrationdate = reader.GetString("Registrationdate"),
                            Email = reader.GetString("Email"),
                            Status = reader.GetString("Status"),
                            AccessPolicy = reader.GetString("AccessPolicy"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_UserProfile_>> GetByGroupAsync(getByIdInput input)
        {
            List<_GET_UserProfile_> list = new List<_GET_UserProfile_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "SELECT * FROM [dbo].[GET_UserProfile]('" + input.PKID + "' )";

              

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_UserProfile_
                        {
                            UserAutoID = reader.GetString("UserAutoID"),
                            Username = reader.GetString("Username"),
                            Mobile = reader.GetString("Mobile"),
                            Password = reader.GetString("Password"),
                            Registrationdate = reader.GetString("Registrationdate"),
                            Email = reader.GetString("Email"),
                            Status = reader.GetString("Status"),
                            AccessPolicy = reader.GetString("AccessPolicy"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
