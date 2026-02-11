using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_PBillVoucherRepository : ISP_PBillVoucherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_PBillVoucherRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_PBillVoucher> input)
        {
            List<ApiResponseMessage> list = new List<ApiResponseMessage>();
            foreach (var SP_PBillVoucher in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_PBillVoucher]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@PBAID";
                    parameter.Value = SP_PBillVoucher.PBAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_PBillVoucher.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OrganizationID";
                    parameter.Value = SP_PBillVoucher.OrganizationID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DivisionID";
                    parameter.Value = SP_PBillVoucher.DivisionID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherTID";
                    parameter.Value = SP_PBillVoucher.VoucherTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherDate";
                    parameter.Value = SP_PBillVoucher.VoucherDate;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CREDITACC";
                    parameter.Value = SP_PBillVoucher   .CREDITACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PURCHASEACC";
                    parameter.Value = SP_PBillVoucher.PURCHASEACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CASHACC";
                    parameter.Value = SP_PBillVoucher.CASHACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CHECKACC";
                    parameter.Value = SP_PBillVoucher.CHECKACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ONLINEACC";
                    parameter.Value = SP_PBillVoucher.ONLINEACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CGSTACC";
                    parameter.Value = SP_PBillVoucher.CGSTACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SGSTACC";
                    parameter.Value = SP_PBillVoucher.SGSTACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@IGSTACC";
                    parameter.Value = SP_PBillVoucher.IGSTACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ReferenceTID";
                    parameter.Value = SP_PBillVoucher.ReferenceTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ReferenceKey";
                    parameter.Value = SP_PBillVoucher.ReferenceKey;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@Narration";
                    parameter.Value = SP_PBillVoucher.Narration;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRPKEY";
                    parameter.Value = SP_PBillVoucher.GRPKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OID";
                    parameter.Value = SP_PBillVoucher.OID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@PAYMENTMODE";
                    parameter.Value = SP_PBillVoucher.PAYMENTMODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VCTYPE";
                    parameter.Value = SP_PBillVoucher.VCTYPE;
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
