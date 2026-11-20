using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_FARMERBILLRepository : IGET_FARMERBILLRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FARMERBILLRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FARMERBILL>> getasync(GET_FARMERBILLDomain input)
        {
            List<GET_FARMERBILL> list = new List<GET_FARMERBILL>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FARMERBILL](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VBKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FARMERBILL
                        {
                            VBKID = reader.GetString("VBKID"),
                           
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            JAALI = reader.GetInt32("JAALI"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            WEIGHTDIFFRENCE = reader.GetString("WEIGHTDIFFRENCE"),
                            VSHORTNAME = reader.GetString("VSHORTNAME"),
                            VCOMPNAME = reader.GetString("VCOMPNAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            VAADHAR = reader.GetString("VAADHAR"),
                            VBANKNAME = reader.GetString("VBANKNAME"),
                            VACCOUNTNUMBER = reader.GetString("VACCOUNTNUMBER"),
                            VIFSCCODE = reader.GetString("VIFSCCODE"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                            CROPTYPE = reader.GetString("CROPTYPE"),
                            RATE = reader.GetDouble("RATE"),
                            DETAIL_TOTALAMOUNT = reader.GetDouble("DETAIL_TOTALAMOUNT"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            TOTALLOSS = reader.GetDouble("TOTALLOSS"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            VNAME = reader.GetString("VNAME"),
                            FNAME = reader.GetString("FNAME"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
          
                        });
                    }
                }
            }
            return list;
        }
    }
}
