using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropInvertDataRepository : IGET_CropInvertDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropInvertDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropInvertData>> getasync(GET_CropInvertDataDomain input)
        {
            List<GET_CropInvertData> list = new List<GET_CropInvertData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_CropInvertData]('" + input.SHAID + "' ,  '" + input.KEYWORD + "'   ,'" + input.DATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertData
                        {
                            CAREA = reader.GetString("CAREA"),
                            CTYPE = reader.GetString("CTYPE"),
                            SHDATE = reader.GetString("SHDATE"),    
                            CROPNAME = reader.GetString("CROPNAME"),
                            NARR = reader.GetString("NARR"),
                            SHAID = reader.GetString("SHAID"),
                            SHDAID = reader.GetString("SHDAID"),
                          
                            VAID = reader.GetString("VAID"),
                            FAID = reader.GetString("FAID"),
                            PACKER = reader.GetString("PACKER"),
                            WORKER = reader.GetString("WORKER"),
                            VEHNO = reader.GetString("VEHNO"),
                            PSIZE = reader.GetString("PSIZE"),
                            VDAID = reader.GetString("VDAID"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            
                           

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CropInvertData>> getkeyword(GET_CropInvertDataDomain input)
        {
            List<GET_CropInvertData> list = new List<GET_CropInvertData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_CropInvertData]('" + input.SHAID + "' ,  '" + input.KEYWORD + "'   ,'" + input.DATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertData
                        {
                            CAREA = reader.GetString("CAREA"),
                            CTYPE = reader.GetString("CTYPE"),
                            SHDATE = reader.GetString("SHDATE"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            NARR = reader.GetString("NARR"),
                            SHAID = reader.GetString("SHAID"),
                            SHDAID = reader.GetString("SHDAID"),

                            VAID = reader.GetString("VAID"),
                            FAID = reader.GetString("FAID"),
                            PACKER = reader.GetString("PACKER"),
                            WORKER = reader.GetString("WORKER"),
                            VEHNO = reader.GetString("VEHNO"),
                            PSIZE = reader.GetString("PSIZE"),
                            VDAID = reader.GetString("VDAID"),
                            FARMERNAME = reader.GetString("FARMERNAME")


                        });
                    }
                }
            }
            return list;
        }
    }
}
