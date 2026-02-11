using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropInvertData1Repository : IGET_CropInvertData1Repository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropInvertData1Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_CropInvertData1>> getasync(GET_CropInvertData1Domain input)
        {
            List<GET_CropInvertData1> list = new List<GET_CropInvertData1>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CropInvertData1]('" + input.SHAID + "' ,  '" + input.KEYWORD + "'   ,'" + input.DATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertData1
                        { 
                            CAREA = reader.GetString("CAREA"),
                            SHAID = reader.GetString("SHAID"),
                            SHDAID = reader.GetString("SHDAID"),
                            SHDATE = reader.GetDateTime("SHDATE"),
                            VAID = reader.GetString("VAID"),
                            FAID = reader.GetString("FAID"),
                            CTYPE = reader.GetString("CTYPE"),
                            NARR = reader.GetString("NARR"),
                            VDAID = reader.GetString("VDAID"),
                            PACKER = reader.GetString("PACKER"),
                            WORKER = reader.GetString("WORKER"),
                            VEHNO = reader.GetString("VEHNO"),
                            PSIZE = reader.GetString("PSIZE"),
                            PSIZENAME = reader.GetString("PSIZENAME"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            CROPNAME = reader.GetString("CROPNAME")

                        });
                    }
                }
            }
            return list;
        }
    }
}
