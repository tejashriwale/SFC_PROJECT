using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_AccessRights_Repository : I_GET_AccessRights_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_AccessRights_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_AccessRights_>> AccessRightAllAsync( )
        {
            List<_GET_AccessRights_> list = new List<_GET_AccessRights_>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AccessRights]('%')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_AccessRights_
                        {
                            AccessRightsAutoID1 = reader.GetString("AccessRightsAutoID1"),
                            AccessRightID = reader.GetString("AccessRightID"),
                            AccessRightTitle = reader.GetString("AccessRightTitle"),
                            AccessRightDescription = reader.GetString("AccessRightDescription"),
                            ApplicationGroup = reader.GetString("ApplicationGroup"),
                            AccessRightGroup = reader.GetString("AccessRightGroup"),
                            IsAdminRight = reader.GetBoolean("IsAdminRight"),
                        });
                    }
                }

            }
            return list;
        }

        public async Task<List<_GET_AccessRights_>> GetByGroupAsync(_GET_AccessRights_Domain input)
        {
            List<_GET_AccessRights_> list = new List<_GET_AccessRights_>();

          using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            
          {
                command.CommandText = "SELECT * FROM [dbo].[GET_AccessRights]('"+ input.AccessRightsAutoID1 + "' , '"+input.KEYWORD+ "' , '"+input.COMPANYID+"')";

               
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new _GET_AccessRights_
                        {
                            AccessRightsAutoID1 = reader.GetString("AccessRightsAutoID1"),
                            AccessRightID = reader.GetString("AccessRightID"),
                            AccessRightTitle = reader.GetString("AccessRightTitle"),
                            AccessRightDescription = reader.GetString("AccessRightDescription"),
                            ApplicationGroup = reader.GetString("ApplicationGroup"),
                            AccessRightGroup = reader.GetString("AccessRightGroup"),
                            IsAdminRight = reader.GetBoolean("IsAdminRight"),
                        });
                    }
                }
           }
          return list;

        }
    }
}
