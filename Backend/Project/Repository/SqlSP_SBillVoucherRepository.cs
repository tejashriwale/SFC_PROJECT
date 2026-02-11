using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_SBillVoucherRepository : ISP_SBillVoucherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_SBillVoucherRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_SBillVoucher> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_SBillVoucher in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_SBillVoucher]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@SBAID";
                    parameter.Value = SP_SBillVoucher.SBAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_SBillVoucher.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OrganizationID";
                    parameter.Value = SP_SBillVoucher.OrganizationID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DivisionID";
                    parameter.Value = SP_SBillVoucher.DivisionID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherTID";
                    parameter.Value = SP_SBillVoucher.VoucherTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherDate";
                    parameter.Value = SP_SBillVoucher.VoucherDate;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CREDITACC";
                    parameter.Value = SP_SBillVoucher.CREDITACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PURCHASEACC";
                    parameter.Value = SP_SBillVoucher.PURCHASEACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CASHACC";
                    parameter.Value = SP_SBillVoucher.CASHACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CHECKACC";
                    parameter.Value = SP_SBillVoucher.CHECKACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ONLINEACC";
                    parameter.Value = SP_SBillVoucher.ONLINEACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CGSTACC";
                    parameter.Value = SP_SBillVoucher.CGSTACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SGSTACC";
                    parameter.Value = SP_SBillVoucher.SGSTACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@IGSTACC";
                    parameter.Value = SP_SBillVoucher.IGSTACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ReferenceTID";
                    parameter.Value = SP_SBillVoucher.ReferenceTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ReferenceKey";
                    parameter.Value = SP_SBillVoucher.ReferenceKey;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@Narration";
                    parameter.Value = SP_SBillVoucher.Narration;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRPKEY";
                    parameter.Value = SP_SBillVoucher.GRPKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OID";
                    parameter.Value = SP_SBillVoucher.OID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PAYMENTMODE";
                    parameter.Value = SP_SBillVoucher.PAYMENTMODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VCTYPE";
                    parameter.Value = SP_SBillVoucher.VCTYPE;
                    command.Parameters.Add(parameter);

                    dbContext.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new ApiResponseMessage
                            {
                                IsSuccessful = reader.GetInt32("IsSuccessful"),
                                ResponseCode = reader.GetString("ResponseCode"),
                                ResponseMessage = reader.GetString("ResponseMessage"),
                                ResponseValues = reader.GetString("ResponseValues")
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
