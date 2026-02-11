using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_TransectionVoucherRepository : ISP_TransectionVoucherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_TransectionVoucherRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_TransectionVoucher> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            {
                foreach (var SP_TransectionVoucher in input)
                    using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "[SP_TransectionVoucher]";
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@VoucherAID";
                        parameter.Value = SP_TransectionVoucher.VoucherAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@UAID";
                        parameter.Value = SP_TransectionVoucher.UAID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@OrganizationID";
                        parameter.Value = SP_TransectionVoucher.OrganizationID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@DivisionID";
                        parameter.Value = SP_TransectionVoucher.DivisionID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@VoucherTID";
                        parameter.Value = SP_TransectionVoucher.VoucherTID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@VoucherDate";
                        parameter.Value = SP_TransectionVoucher.VoucherDate;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@YAPARIACC";
                        parameter.Value = SP_TransectionVoucher.YAPARIACC;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@CASHACC";
                        parameter.Value = SP_TransectionVoucher.CASHACC;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@CHECKACC";
                        parameter.Value = SP_TransectionVoucher.CHECKACC;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@ONLINEACC";
                        parameter.Value = SP_TransectionVoucher.ONLINEACC;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@VoucherAmount";
                        parameter.Value = SP_TransectionVoucher.VoucherAmount;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@ReferenceTID";
                        parameter.Value = SP_TransectionVoucher.ReferenceTID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@ReferenceKey";
                        parameter.Value = SP_TransectionVoucher.ReferenceKey;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@Narration";
                        parameter.Value = SP_TransectionVoucher.Narration;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@GRPKEY";
                        parameter.Value = SP_TransectionVoucher.GRPKEY;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@OID";
                        parameter.Value = SP_TransectionVoucher.OID;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@TRNTYPE";
                        parameter.Value = SP_TransectionVoucher.TRNTYPE;
                        command.Parameters.Add(parameter);

                        parameter = command.CreateParameter();
                        parameter.ParameterName = "@VCTYPE";
                        parameter.Value = SP_TransectionVoucher.VCTYPE;
                        command.Parameters.Add(parameter);

                        dbContext.Database.OpenConnection();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                result.Add(new ApiResponseMessage
                                {
                                    ResponseCode = reader.GetString("ResponseCode"),
                                    ResponseMessage = reader.GetString("ResponseMessage"),
                                    ResponseValues = reader.GetString("ResponseValues"),
                                    IsSuccessful = reader.GetInt32("IsSuccessful")
                                });
                            }
                        }
                    }
            }
            return result;
        }
    }
}
