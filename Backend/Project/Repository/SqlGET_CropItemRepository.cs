using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CropItemRepository : IGET_CropItemRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropItemRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CropItem>> getasync(GET_CropItemDomain input)
        {
            List<GET_CropItem> list = new List<GET_CropItem>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20  * FROM [GET_CropItem](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ITEMAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropItem
                        {
                            ITEMAID = reader.GetString("ITEMAID"),
                            ITEMID = reader.GetString("ITEMID"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            ITEMNE = reader.GetString("ITEMNE"),
                            STATUS = reader.GetString("STATUS"),
                            ITEMPHOTO = reader.GetString("ITEMPHOTO"),
                            EXCharge = reader.GetDouble("EXCharge")


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CropItem>> getkeyword(GET_CropItemDomain input)
        {
            List<GET_CropItem> list = new List<GET_CropItem>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropItem](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.ITEMAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropItem
                        {
                            ITEMAID = reader.GetString("ITEMAID"),
                            ITEMID = reader.GetString("ITEMID"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            ITEMNE = reader.GetString("ITEMNE"),
                            STATUS = reader.GetString("STATUS"),
                            ITEMPHOTO = reader.GetString("ITEMPHOTO"),
                            EXCharge = reader.GetDouble("EXCharge")


                        });
                    }
                }
            }
            return list;
        }
    }
}
