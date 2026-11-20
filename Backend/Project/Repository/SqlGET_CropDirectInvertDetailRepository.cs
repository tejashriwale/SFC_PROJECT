using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropDirectInvertDetailRepository : IGET_CropDirectInvertDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropDirectInvertDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropDirectInvertDetail>> getasync(GET_CropDirectInvertDetailDomain input)
        {
            List<GET_CropDirectInvertDetail> list = new List<GET_CropDirectInvertDetail>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropDirectInvertDetail]( @p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CROPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropDirectInvertDetail
                        {
                           CDAID = reader.GetString("CDAID"),
                           CGRADE = reader.GetString("CGRADE"),
                           CRATECOUNT = reader.GetString("CRATECOUNT"),
                           PACKER = reader.GetString("PACKER"),
                           PACKINGSIZE = reader.GetString("PACKINGSIZE"),
                           SHDAID = reader.GetString("SHDAID"),
                           SHSDATE = reader.GetString("SHSDATE"),
                           SHAID = reader.GetString("SHAID"),
                           VDAID = reader.GetString("VDAID"),
                           CROPID = reader.GetString("CROPID"),
                           CROPINVERT = reader.GetString("CROPINVERT"),
                           PQUNTITY = reader.GetString("PQUNTITY"),
                           PSIZE = reader.GetString("PSIZE"),
                           VEHNO = reader.GetString("VEHNO"),
                           WEIGHT = reader.GetDouble("WEIGHT"),
                           RATE = reader.GetDouble("RATE"),
                           WORKER = reader.GetString("WORKER"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
