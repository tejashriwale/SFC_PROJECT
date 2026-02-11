using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class Sql_GET_StateCodeMasters_Repository : I_GET_StateCodeMasters_Repository
    {
        private readonly ApplicationDbContext dbContext;

        public Sql_GET_StateCodeMasters_Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<_GET_StateCodeMasters_>> getasync(DomainMaster input)
        {
            List<_GET_StateCodeMasters_> list = new List<_GET_StateCodeMasters_> ();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select Top 20 * from [GET_StateCodeMasters]('%' , '"+input.KEYWORD+"' )";

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                       

                        list.Add(new _GET_StateCodeMasters_
                        {
                            SAID = reader.GetString("SAID"),
                            SCIRCLENAME = reader.GetString("SCIRCLENAME"),
                            SREGIONNAME = reader.GetString("SREGIONNAME"),
                            SDIVISIONNAME = reader.GetString("SDIVISIONNAME"),
                            SOFFICENAME = reader.GetString("SOFFICENAME"),
                            SPINCODE = reader.GetString("SPINCODE"),
                            SOFFICETYPE = reader.GetString("SOFFICETYPE"),
                            SDELIVERY = reader.GetString("SDELIVERY"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            SSTATECODE = reader.GetString("SSTATECODE"),
                            SLATITUDE = reader.GetDouble("SLATITUDE"),
                            SLONTITUDE = reader.GetDouble("SLONTITUDE"),
                           


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_StateCodeMasters_>> GetByGroupAsync(DomainMaster input)
        {
            List<_GET_StateCodeMasters_> list = new List<_GET_StateCodeMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())

            {
                command.CommandText = "select Top 20 * from [GET_StateCodeMasters]('"+input.PKID+"','"+input.KEYWORD+"')";


                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new _GET_StateCodeMasters_
                        {
                            SAID = reader.GetString("SAID"),
                            SCIRCLENAME = reader.GetString("SCIRCLENAME"),
                            SREGIONNAME = reader.GetString("SREGIONNAME"),
                            SDIVISIONNAME = reader.GetString("SDIVISIONNAME"),
                            SOFFICENAME = reader.GetString("SOFFICENAME"),
                            SPINCODE = reader.GetString("SPINCODE"),
                            SOFFICETYPE = reader.GetString("SOFFICETYPE"),
                            SDELIVERY = reader.GetString("SDELIVERY"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            SSTATECODE = reader.GetString("SSTATECODE"),
                            SLATITUDE = reader.GetDouble("SLATITUDE"),
                            SLONTITUDE = reader.GetDouble("SLONTITUDE"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<_GET_StateCodeMasters_>> serchasync(DomainMaster input)
        {
            List<_GET_StateCodeMasters_> list = new List<_GET_StateCodeMasters_>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select  * from [GET_StateCodeMasters]('" + input.PKID + "' , '" + input.KEYWORD + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new _GET_StateCodeMasters_
                        {
                            SAID = reader.GetString("SAID"),
                            SCIRCLENAME = reader.GetString("SCIRCLENAME"),
                            SREGIONNAME = reader.GetString("SREGIONNAME"),
                            SDIVISIONNAME = reader.GetString("SDIVISIONNAME"),
                            SOFFICENAME = reader.GetString("SOFFICENAME"),
                            SPINCODE = reader.GetString("SPINCODE"),
                            SOFFICETYPE = reader.GetString("SOFFICETYPE"),
                            SDELIVERY = reader.GetString("SDELIVERY"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            SSTATECODE = reader.GetString("SSTATECODE"),
                            SLATITUDE = reader.GetDouble("SLATITUDE"),
                            SLONTITUDE = reader.GetDouble("SLONTITUDE"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
