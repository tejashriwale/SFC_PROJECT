using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_ShopAlotRepository : IGET_ShopAlotRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ShopAlotRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ShopAlot>> getasync(GET_ShopAlotDomain input)
        {
            List<GET_ShopAlot> list = new List<GET_ShopAlot>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_ShopAlot]('" + input.SHID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ShopAlot
                        {
                            SHID = reader.GetString("SHID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            SHOPNAME = reader.GetString("SHOPNAME"),
                            SHOPID = reader.GetString("SHOPID"),
                            SDATE = reader.GetString("SDATE"),
                            EDATE = reader.GetString("EDATE"),
                            DEPOSIT = reader.GetDouble("DEPOSIT"),
                            RENT = reader.GetDouble("RENT"),
                            VNAME	 = reader.GetString("VNAME"),
                            VAPNAME = reader.GetString("VAPNAME"),
                            STORENAME = reader.GetString("STORENAME"),
                            SHOPN = reader.GetString("SHOPN"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_ShopAlot>> getkeyword(GET_ShopAlotDomain input)
        {
            List<GET_ShopAlot> list = new List<GET_ShopAlot>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ShopAlot]('" + input.SHID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ShopAlot
                        {
                            SHID = reader.GetString("SHID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            SHOPNAME = reader.GetString("SHOPNAME"),
                            SHOPID = reader.GetString("SHOPID"),
                            SDATE = reader.GetString("SDATE"),
                            EDATE = reader.GetString("EDATE"),
                            DEPOSIT = reader.GetDouble("DEPOSIT"),
                            RENT = reader.GetDouble("RENT"),
                            VNAME = reader.GetString("VNAME"),
                            VAPNAME = reader.GetString("VAPNAME"),
                            STORENAME = reader.GetString("STORENAME"),
                            SHOPN = reader.GetString("SHOPN"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
