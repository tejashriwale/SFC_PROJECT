using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CreateBillAppRepository : IGET_CreateBillAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CreateBillAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CreateBillApp>> getasync(GET_CreateBillAppDomain input)
        {
            List<GET_CreateBillApp> list = new List<GET_CreateBillApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CreateBillApp](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CreateBillApp
                        {
                            	BAID = reader.GetString("BAID"),
                            	MAID = reader.GetString("MAID"),
                            	BILLNO	 = reader.GetInt32("BILLNO"),
                            	BTOKANNO		 = reader.GetString("BTOKANNO"),
                            	DATE	 = reader.GetString("DATE"),
                            	SEVA = reader.GetString("SEVA"),
                            	VYAPARINAME	 = reader.GetString("VYAPARINAME"),
                            	WEIGHT	  = reader.GetDouble("WEIGHT"),
                            	RATE	 = reader.GetDouble("RATE"),
                            	AMOUNT	 = reader.GetDouble("AMOUNT"),
                            	TOTALWEIGHT	 = reader.GetDouble("TOTALWEIGHT"),
                            	TOTALAMOUNT	 = reader.GetDouble("TOTALAMOUNT"),
                            	TOTALCOST	 = reader.GetDouble("TOTALCOST"),
                            	REMAININGAMOUNT	 = reader.GetDouble("REMAININGAMOUNT"),
                                CROP_TYPE = reader.GetString("CROP_TYPE"),
                            	CROPLABEL = reader.GetString("CROPLABEL"),
                            	VNAME	  = reader.GetString("VNAME"),
                            	FULLNAME = reader.GetString("FULLNAME"),
                                CHEQUEAPPROVED = reader.GetString("CHEQUEAPPROVED"),
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
