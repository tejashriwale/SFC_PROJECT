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
                command.CommandText = "SELECT * FROM [GET_CropInvertData1](@p0 ,  @p1   ,@p2 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p2); }
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
