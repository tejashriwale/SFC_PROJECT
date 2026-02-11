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
                command.CommandText = "select Top 20 * from [GET_HSNMasters]('%' ,'" + input.KEYWORD + "')";
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
                command.CommandText = "select Top 20 * FROM [dbo].[GET_HSNMasters]('" + input.PKID + "' , '" + input.KEYWORD + "' )";
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
                command.CommandText = "select Top 20 *FROM [dbo].[GET_HSNMasters]('" + input.PKID + "' , '" + input.KEYWORD + "' )";
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
