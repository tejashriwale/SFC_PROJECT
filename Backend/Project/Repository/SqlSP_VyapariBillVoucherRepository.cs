using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_VyapariBillVoucherRepository : ISP_VyapariBillVoucherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_VyapariBillVoucherRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<ApiResponseMessage>> getasync(List<SP_VyapariBillVoucher> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_VyapariBillVoucher in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_VyapariBillVoucher]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@SEVA";
                    parameter.Value = SP_VyapariBillVoucher.SEVA;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherAmount";
                    parameter.Value = SP_VyapariBillVoucher.VoucherAmount;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VREFF";
                    parameter.Value = SP_VyapariBillVoucher.VREFF;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@MAID";
                    parameter.Value = SP_VyapariBillVoucher.MAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OrganizationID";
                    parameter.Value = SP_VyapariBillVoucher.OrganizationID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DivisionID";
                    parameter.Value = SP_VyapariBillVoucher.DivisionID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherDate";
                    parameter.Value = SP_VyapariBillVoucher.VoucherDate;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_VyapariBillVoucher.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OID";
                    parameter.Value = SP_VyapariBillVoucher.OID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VYAPARIACC";
                    parameter.Value = SP_VyapariBillVoucher.VYAPARIACC;
                    command.Parameters.Add(parameter);

                    dbContext.Database.OpenConnection();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new ApiResponseMessage
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
            return result;
        }
    }
}
