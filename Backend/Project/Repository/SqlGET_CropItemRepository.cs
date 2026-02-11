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
                command.CommandText = "SELECT Top 20  * FROM [GET_CropItem]('" + input.ITEMAID + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "SELECT  * FROM [GET_CropItem]('" + input.ITEMAID + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
