using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_MarketStoreRepository : IGET_MarketStoreRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_MarketStoreRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_MarketStore>> getasync(GET_MarketStoreDomain input)
        {
            List<GET_MarketStore> list = new List<GET_MarketStore>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_MarketStore](@p0 , @p1 , @p2 , @p3)order by DATE desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STORID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_MarketStore
                        {
                            STORID = reader.GetString("STORID"),
                            STOREID = reader.GetString("STOREID"),
                            SDATE = reader.GetString("SDATE"),
                            STORENAME = reader.GetString("STORENAME"),
                            STORERENT = reader.GetDouble("STORERENT"),
                            STORELOCATION = reader.GetString("STORELOCATION"),
                            STATUS = reader.GetString("STATUS"),
                            STORECAPACITY = reader.GetInt32("STORECAPACITY"),
                            STOREDEPOSIT = reader.GetDouble("STOREDEPOSIT"),
                            VSTATUS = reader.GetString("VSTATUS"),
                            SHOPLOC = reader.GetString("SHOPLOC"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_MarketStore>> getkeyword(GET_MarketStoreDomain input)
        {
            List<GET_MarketStore> list = new List<GET_MarketStore>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_MarketStore](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STORID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_MarketStore
                        {
                            STORID = reader.GetString("STORID"),
                            STOREID = reader.GetString("STOREID"),
                            SDATE = reader.GetString("SDATE"),
                            STORENAME = reader.GetString("STORENAME"),
                            STORERENT = reader.GetDouble("STORERENT"),
                            STORELOCATION = reader.GetString("STORELOCATION"),
                            STATUS = reader.GetString("STATUS"),
                            STORECAPACITY = reader.GetInt32("STORECAPACITY"),
                            STOREDEPOSIT = reader.GetDouble("STOREDEPOSIT"),
                            VSTATUS = reader.GetString("VSTATUS"),
                            SHOPLOC = reader.GetString("SHOPLOC"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
