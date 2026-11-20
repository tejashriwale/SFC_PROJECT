using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CreateFarmerBillVyapariRepository : IGET_CreateFarmerBillVyapariRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CreateFarmerBillVyapariRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CreateFarmerBillVyapari>> getasync(GET_CreateFarmerBillVyapariDomain input)
        {
            List<GET_CreateFarmerBillVyapari> list = new List<GET_CreateFarmerBillVyapari>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CreateFarmerBillVyapari](@p0, @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.BILLNO) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CreateFarmerBillVyapari
                        {
                            	
                            	BILLNO	 = reader.GetInt32("BILLNO"),
                            	
                            	DATE	 = reader.GetString("DATE"),
                            	SEVA = reader.GetString("SEVA"),
                            	VYAPARINAME	 = reader.GetString("VYAPARINAME"),
                            	WEIGHT	  = reader.GetDouble("WEIGHT"),
                            	RATE	 = reader.GetDouble("RATE"),
                            	AMOUNT	 = reader.GetDouble("AMOUNT"),
                            	TOTALWEIGHT	 = reader.GetDouble("TOTALWEIGHT"),
                            	TOTALAMOUNT	 = reader.GetDouble("TOTALAMOUNT"),
                            	FARMERNAME = reader.GetString("FARMERNAME"),
                            	TOTALLOSS = reader.GetDouble("TOTALLOSS"),
                            	FNAME = reader.GetString("FNAME"),
                            	JAALI = reader.GetInt32("JAALI"),
                            	TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                                
                            	REMAININGAMOUNT	 = reader.GetDouble("REMAININGAMOUNT"),
                              
                            	VNAME	  = reader.GetString("VNAME"),

                           
                        });
                    }
                }
            }
            return list;
        }
    }
    
}
