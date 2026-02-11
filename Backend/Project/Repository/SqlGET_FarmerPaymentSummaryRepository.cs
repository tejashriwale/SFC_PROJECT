using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerPaymentSummaryRepository : IGET_FarmerPaymentSummaryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerPaymentSummaryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerPaymentSummary>> getasync(GET_FarmerPaymentSummaryDomain input)
        {
            List<GET_FarmerPaymentSummary> list = new List<GET_FarmerPaymentSummary>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"SELECT * FROM [GET_FarmerPaymentSummary]('{input.REFAID}', '{input.COMPANYID}', '{input.DEPTID}')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerPaymentSummary
                        {
                            TRAID = reader.GetString("TRAID"),
                            TRANSACTIONDATE = reader.GetString("TRANSACTIONDATE"),
                            PAYMENTDATE = reader.GetString("PAYMENTDATE"),
                            TRNSR = reader.GetInt32("TRNSR"),
                            REFAID = reader.GetString("REFAID"),
                            MAID = reader.GetString("MAID"),
                            PAYMENTAMOUNT = reader.GetDouble("PAYMENTAMOUNT"),
                            NARRATION = reader.GetString("NARRATION"),
                            TRNSID = reader.GetString("TRNSID"),
                            TRNTID = reader.GetString("TRNTID")
                        });
                    }
                }
            }
            return list;
        }

        public async Task<bool> UpdatePaymentDateAsync(UpdatePaymentDateDomain input)
        {
            try
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    // Update both PAYMENTDT and TRNDT to the same date
                    command.CommandText = @"
                        UPDATE tblAllTransections
                        SET PAYMENTDT = @PaymentDate,
                            TRNDT = @PaymentDate
                        WHERE TRAID = @TRAID
                        AND COMPANYID = @COMPANYID
                        AND DEPTID = @DEPTID
                        AND TRNSID = '20'";

                    var paramPaymentDate = command.CreateParameter();
                    paramPaymentDate.ParameterName = "@PaymentDate";
                    paramPaymentDate.Value = DateTime.Parse(input.PAYMENTDATE);
                    command.Parameters.Add(paramPaymentDate);

                    var paramTRAID = command.CreateParameter();
                    paramTRAID.ParameterName = "@TRAID";
                    paramTRAID.Value = input.TRAID;
                    command.Parameters.Add(paramTRAID);

                    var paramCompanyID = command.CreateParameter();
                    paramCompanyID.ParameterName = "@COMPANYID";
                    paramCompanyID.Value = input.COMPANYID;
                    command.Parameters.Add(paramCompanyID);

                    var paramDeptID = command.CreateParameter();
                    paramDeptID.ParameterName = "@DEPTID";
                    paramDeptID.Value = input.DEPTID;
                    command.Parameters.Add(paramDeptID);

                    dbContext.Database.OpenConnection();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
