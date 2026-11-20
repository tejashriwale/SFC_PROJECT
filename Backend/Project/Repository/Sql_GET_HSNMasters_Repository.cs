using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_HSNMasters_Repository : I_GET_HSNMasters_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_HSNMasters_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_HSNMasters_>> getasync(DomainMaster input)
        {
           List<_GET_HSNMasters_> list = new List<_GET_HSNMasters_>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_HSNMasters]('%' ,@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new _GET_HSNMasters_
                        {
                            HAID = reader.GetString("HAID"),
                            HCODE = reader.GetString("HCODE"),
                            HTYPE = reader.GetString("HTYPE"),
                            HDESCRIPTION = reader.GetString("HDESCRIPTION"),
                            HIGST = reader.GetDouble("HIGST"),
                            HSGST = reader.GetDouble("HSGST"),
                            HCGST = reader.GetDouble("HCGST"),
                            HCESS = reader.GetDouble("HCESS"),
                            ImplicationTitle = reader.GetString("ImplicationTitle"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_HSNMasters_>> GetByGroupAsync(DomainMaster input)
        {
            List<_GET_HSNMasters_> list = new List<_GET_HSNMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 * FROM [dbo].[GET_HSNMasters](@p0 , @p1 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                await dbContext.Database.OpenConnectionAsync();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_HSNMasters_
                        {
                            HAID = reader.GetString("HAID"),
                            HCODE = reader.GetString("HCODE"),
                            HTYPE = reader.GetString("HTYPE"),
                            HDESCRIPTION = reader.GetString("HDESCRIPTION"),
                            HIGST = reader.GetDouble("HIGST"),
                            HSGST = reader.GetDouble("HSGST"),
                            HCGST = reader.GetDouble("HCGST"),
                            HCESS = reader.GetDouble("HCESS"),
                            ImplicationTitle = reader.GetString("ImplicationTitle"),
                           

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_HSNMasters_>> Serchasync(DomainMaster input)
        {
            List<_GET_HSNMasters_> list = new List<_GET_HSNMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 *FROM [dbo].[GET_HSNMasters](@p0 , @p1 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_HSNMasters_
                        {
                            HAID = reader.GetString("HAID"),
                            HCODE = reader.GetString("HCODE"),
                            HTYPE = reader.GetString("HTYPE"),
                            HDESCRIPTION = reader.GetString("HDESCRIPTION"),
                            HIGST = reader.GetDouble("HIGST"),
                            HSGST = reader.GetDouble("HSGST"),
                            HCGST = reader.GetDouble("HCGST"),
                            HCESS = reader.GetDouble("HCESS"),
                            ImplicationTitle = reader.GetString("ImplicationTitle"),
                           

                        });
                    }

                }
            }
            return list;
          
        }
    }
}
