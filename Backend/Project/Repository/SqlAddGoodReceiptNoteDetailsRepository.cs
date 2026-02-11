using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlAddGoodReceiptNoteDetailsRepository : IAddGoodReceiptNoteDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlAddGoodReceiptNoteDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> getasync(List<AddGoodReceiptNoteDetails> addGoodReceipt)
        {
           List<ApiResponseMessage>result = new List<ApiResponseMessage>();
            foreach (var AddGoodReceiptNoteDetails in addGoodReceipt)
            {
                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "[SP_AddUpdGoodReceiptNoteDetails]";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDAID";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNAID";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNAID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDPRODUCT";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDPRODUCT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDEPARTMENT";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDEPARTMENT;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDUOM";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDUOM;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDCHALLANQUANTITY";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDCHALLANQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDSHORTQUANTITY";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDSHORTQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDEXCESSQUANTITY";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDEXCESSQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDREJECTEDQUANTITY";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDREJECTEDQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDACCEPTEDQUANTITY";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDACCEPTEDQUANTITY;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDRATE";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDRATE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDDISCOUNT";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDDISCOUNT;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@GRNDTOTALVALUE";
                    parameter.Value = AddGoodReceiptNoteDetails.GRNDTOTALVALUE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@HSNCODE";
                    parameter.Value = AddGoodReceiptNoteDetails.HSNCODE;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@COMPANYID";
                    parameter.Value = AddGoodReceiptNoteDetails.COMPANYID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@DEPTID";
                    parameter.Value = AddGoodReceiptNoteDetails.DEPTID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@BATCHNO";
                    parameter.Value = AddGoodReceiptNoteDetails.BATCHNO;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@EXPIRYDATE";
                    parameter.Value = AddGoodReceiptNoteDetails.EXPIRYDATE;
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
