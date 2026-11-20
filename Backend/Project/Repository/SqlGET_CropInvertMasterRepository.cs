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
                command.CommandText = "SELECT Top 20 * FROM [GET_CropInvertMaster](@p0 ,  @p1   ,@p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p2); }
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
                command.CommandText = "SELECT  * FROM [GET_CropInvertMaster](@p0 ,  @p1   ,@p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p2); }
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
