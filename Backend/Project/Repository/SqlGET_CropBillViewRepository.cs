using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropBillViewRepository : IGET_CropBillViewRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropBillViewRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropBillView>> getasync(GET_CropBillViewDomain input)
        {
            List<GET_CropBillView> list = new List<GET_CropBillView>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropBillView]( @p0   ,@p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CDAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropBillView
                        {
                           VISITINGDATE = reader.GetString("VISITINGDATE"),
                            CROPID = reader.GetString("CROPID"),
                            CDAID = reader.GetString("CDAID"),
                            CRATECOUNT = reader.GetString("CRATECOUNT"),
                            CROPINVERT = reader.GetString("CROPINVERT"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            PQUNTITY = reader.GetString("PQUNTITY"),
                            VEHNO = reader.GetString("VEHNO"),
                            CVARIETY = reader.GetString("CVARIETY"),
                            RATE = reader.GetDouble("RATE"),
                            DATE = reader.GetString("DATE"),
                             FAID = reader.GetString("FAID"),
                             FARMERNAME = reader.GetString("FARMERNAME")


                        });
                    }
                }
            }
            return list;
        }
    }
}
