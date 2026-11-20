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
                command.CommandText = "SELECT Top 20 * FROM [GET_CropInvertData](@p0 ,  @p1   ,@p2 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p2); }
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
                command.CommandText = "SELECT Top 20 * FROM [GET_CropInvertData](@p0 ,  @p1   ,@p2 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p2); }
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
