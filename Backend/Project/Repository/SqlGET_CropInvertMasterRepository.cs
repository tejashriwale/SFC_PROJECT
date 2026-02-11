using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropInvertMasterRepository : IGET_CropInvertMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropInvertMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<GET_CropInvertMaster>> getasync(GET_CropInvertMasterDomain input)
        {
            List<GET_CropInvertMaster> list = new List<GET_CropInvertMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_CropInvertMaster]('" + input.SHAID + "' ,  '" + input.KEYWORD + "'   ,'" + input.DATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertMaster
                        {
                            SHAID = reader.GetString("SHAID"),
                            SHDATE = reader.GetString("SHDATE"),
                            CAREA = reader.GetString("CAREA"),
                            CTYPE = reader.GetString("CTYPE"),
                            NARR = reader.GetString("NARR"),
                            VAID = reader.GetString("VAID"),
                            VDAID = reader.GetString("VDAID"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            FAID = reader.GetString("FAID"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CARET = reader.GetDouble("CARET"),
                            CROPID = reader.GetString("CROPID"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CropInvertMaster>> getkeyword(GET_CropInvertMasterDomain input)
        {
            List<GET_CropInvertMaster> list = new List<GET_CropInvertMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropInvertMaster]('" + input.SHAID + "' ,  '" + input.KEYWORD + "'   ,'" + input.DATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertMaster
                        {
                            SHAID = reader.GetString("SHAID"),
                            SHDATE = reader.GetString("SHDATE"),
                            CAREA = reader.GetString("CAREA"),
                            CTYPE = reader.GetString("CTYPE"),
                            NARR = reader.GetString("NARR"),
                            VAID = reader.GetString("VAID"),
                            VDAID = reader.GetString("VDAID"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            EPRODUCTION = reader.GetString("EPRODUCTION"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            CONTACTNO = reader.GetString("CONTACTNO"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            CARET = reader.GetDouble("CARET"),
                            CROPID = reader.GetString("CROPID"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
