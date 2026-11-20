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
                command.CommandText = "SELECT * FROM [dbo].[GET_AccessRights](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.AccessRightsAutoID1) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }

               
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
