using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;
using System.Data.Common;

namespace Project.Repository
{
    public class SqlGET_StockLoadingVehicleMasterRepository : IGET_StockLoadingVehicleMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StockLoadingVehicleMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private bool HasColumn(DbDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        public async Task<List<GET_StockLoadingVehicleMaster>> getasync(GET_StockLoadingVehicleMasterDomain input)
        {
            List<GET_StockLoadingVehicleMaster> list = new List<GET_StockLoadingVehicleMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_StockLoadingVehicleMaster]('" + input.SLVMID + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var hasPono = HasColumn(reader, "PONO");
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_StockLoadingVehicleMaster
                        {
                           GRADE1 = reader.GetString("GRADE1"),
                           SLVMID = reader.GetString("SLVMID"),
                           CARETCOUNT = reader.GetString("CARETCOUNT"),
                           TRADER = reader.GetString("TRADER"),
                           CROP = reader.GetString("CROP"),
                           VEHICLENO = reader.GetString("VEHICLENO"),
                           CROP1 = reader.GetString("CROP1"),
                           TRADER1 = reader.GetString("TRADER1"),
                           LDATE = reader.GetString("LDATE"),
                            BILLSTATUS = reader.GetString("BILLSTATUS"),
                            SAMT = reader.GetDouble("SAMT"),
                            PONO = hasPono && !reader.IsDBNull(reader.GetOrdinal("PONO")) ? reader.GetString("PONO") : "",
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_StockLoadingVehicleMaster>> getkeyword(GET_StockLoadingVehicleMasterDomain input)
        {
            List<GET_StockLoadingVehicleMaster> list = new List<GET_StockLoadingVehicleMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_StockLoadingVehicleMaster]('" + input.SLVMID + "' , '" + input.KEYWORD + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var hasPono = HasColumn(reader, "PONO");
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_StockLoadingVehicleMaster
                        {
                            GRADE1 = reader.GetString("GRADE1"),
                            SLVMID = reader.GetString("SLVMID"),
                            CARETCOUNT = reader.GetString("CARETCOUNT"),
                            TRADER = reader.GetString("TRADER"),
                            CROP = reader.GetString("CROP"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            CROP1 = reader.GetString("CROP1"),
                            TRADER1 = reader.GetString("TRADER1"),
                            LDATE = reader.GetString("LDATE"),
                            BILLSTATUS = reader.GetString("BILLSTATUS"),
                            PONO = hasPono && !reader.IsDBNull(reader.GetOrdinal("PONO")) ? reader.GetString("PONO") : "",
                        });
                    }
                }
            }
            return list;
        }
    }
}
