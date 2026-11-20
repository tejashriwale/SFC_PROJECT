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
                command.CommandText = "SELECT Top 20 * FROM [GET_ShopAlot](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT * FROM [GET_ShopAlot](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
