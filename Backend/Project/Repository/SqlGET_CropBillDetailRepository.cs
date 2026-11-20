using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropBillDetailRepository : IGET_CropBillDetailRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropBillDetailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_CropBillDetail>> getasync(GET_CropBillDetailDomain input)
        {
            List<GET_CropBillDetail> list = new List<GET_CropBillDetail>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropBillDetail](@p0, @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CDAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.CROPID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropBillDetail
                        {
                            CROPID = reader.GetString("CROPID"),
                            VISITINGDATE = reader.GetString("VISITINGDATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            CRATECOUNT = reader.GetString("CRATECOUNT"),
                            CROPINVERT = reader.GetString("CROPINVERT"),
                            PQUNTITY = reader.GetString("PQUNTITY"),
                            VEHNO = reader.GetString("VEHNO"),
                            SIZE = reader.GetString("SIZE"),
                            COUNT = reader.GetString("COUNT"),
                            PSIZE = reader.GetString("PSIZE"),
                            PACKER = reader.GetString("PACKER"),
                            PACKERNAME = reader.GetString("PACKERNAME"),
                            WORKER = reader.GetString("WORKER"),
                            WORKERNAME = reader.GetString("WORKERNAME"),
                            RATE = reader.GetDouble("RATE"),
                            ADVANCE = reader.GetDouble("ADVANCE"),



                        });
                    }
                }
            }
            return list;
        }
    }
}
