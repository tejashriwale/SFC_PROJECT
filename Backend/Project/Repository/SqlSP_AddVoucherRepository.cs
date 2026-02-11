using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlSP_AddVoucherRepository : ISP_AddVoucherRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_AddVoucherRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<SP_AddVoucher> input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            foreach (var SP_AddVoucher in input)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddVoucher]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherAID";
                    parameter.Value = SP_AddVoucher.VoucherAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OrganizationID";
                    parameter.Value = SP_AddVoucher.OrganizationID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DivisionID";
                    parameter.Value = SP_AddVoucher.DivisionID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherTID";
                    parameter.Value = SP_AddVoucher.VoucherTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@SerialNumber";
                    parameter.Value = SP_AddVoucher.SerialNumber;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherDate";
                    parameter.Value = SP_AddVoucher.VoucherDate;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DRACC";
                    parameter.Value = SP_AddVoucher.DRACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@CRACC";
                    parameter.Value = SP_AddVoucher.CRACC;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherAmount";
                    parameter.Value = SP_AddVoucher.VoucherAmount;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ReferenceTID";
                    parameter.Value = SP_AddVoucher.ReferenceTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@ReferenceKey";
                    parameter.Value = SP_AddVoucher.ReferenceKey;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@Narration";
                    parameter.Value = SP_AddVoucher.Narration;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRPKEY";
                    parameter.Value = SP_AddVoucher.GRPKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHKEY";
                    parameter.Value = SP_AddVoucher.BATCHKEY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@OID";
                    parameter.Value = SP_AddVoucher.OID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VOUCHERTYPE";
                    parameter.Value = SP_AddVoucher.VOUCHERTYPE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@JAMANAVE";
                    parameter.Value = SP_AddVoucher.JAMANAVE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@VoucherNumber";
                    parameter.Value = SP_AddVoucher.VoucherNumber;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@UAID";
                    parameter.Value = SP_AddVoucher.UAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@STATUS";
                    parameter.Value = SP_AddVoucher.STATUS;
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
