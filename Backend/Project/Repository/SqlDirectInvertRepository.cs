using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;
using System.Data.Common;
using System.Linq; // Added this using directive for FirstOrDefault()

namespace Project.Repository
{
    public class SqlDirectInvertRepository : IDirectInvertRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlDirectInvertRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ApiResponseMessage>> addAsync(DirectInvert input)
        {
            List<ApiResponseMessage> result = new List<ApiResponseMessage>();
            string responseCode = string.Empty;

            var connection = dbContext.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                // ---------- Master Call ----------
                var masterItem = input.CropInvertMaster?.FirstOrDefault();
                if (masterItem != null)
                {
                    using (var command1 = connection.CreateCommand())
                    {
                        command1.Transaction = transaction;
                        command1.CommandText = "[SP_AddUpdCropInvertMaster]";
                        command1.CommandType = CommandType.StoredProcedure;

                        void AddParam(string name, object value)
                        {
                            var p = command1.CreateParameter();
                            p.ParameterName = name;
                            p.Value = value ?? DBNull.Value;
                            command1.Parameters.Add(p);
                        }

                        AddParam("@CROPID", masterItem.CROPID);
                        AddParam("@CRDATE", masterItem.CRDATE);
                        AddParam("@FAID", masterItem.FAID);
                        AddParam("@SHAID", masterItem.SHAID);
                        AddParam("@CONTACTNO", masterItem.CONTACTNO);
                        AddParam("@CTYPE", masterItem.CTYPE);
                        AddParam("@CAREA", masterItem.CAREA);
                        AddParam("@EPRODUCTION", masterItem.EPRODUCTION);
                        AddParam("@CVARIETY", masterItem.CVARIETY);
                        AddParam("@NARR", masterItem.NARR);
                        AddParam("@COMPANYID", masterItem.COMPANYID);
                        AddParam("@DEPTID", masterItem.DEPTID);
                        AddParam("@RATE", masterItem.RATE);
                        AddParam("@ISCOMPLITED", masterItem.ISCOMPLITED);
                        AddParam("@WEIGHT", masterItem.WEIGHT);
                        AddParam("@AMOUNT", masterItem.AMOUNT);
                        AddParam("@CARET", masterItem.CARET);

                        using var reader = await command1.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            responseCode = reader.GetString(reader.GetOrdinal("ResponseCode"));

                            result.Add(new ApiResponseMessage
                            {
                                IsSuccessful = reader.GetInt32(reader.GetOrdinal("IsSuccessful")),
                                ResponseCode = responseCode,
                                ResponseMessage = reader.GetString(reader.GetOrdinal("ResponseMessage")),
                                ResponseValues = reader.GetString(reader.GetOrdinal("ResponseValues"))
                            });
                        }
                    }
                }

                // ---------- Detail Call ----------
                if (responseCode == "SUCCESS" && input.CropInvertDetail != null)
                {
                    foreach (var item in input.CropInvertDetail)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = "[SP_AddUpdCropInvertDetail]";
                            command.CommandType = CommandType.StoredProcedure;

                            void AddParam(string name, object value)
                            {
                                var p = command.CreateParameter();
                                p.ParameterName = name;
                                p.Value = value ?? DBNull.Value;
                                command.Parameters.Add(p);
                            }

                            AddParam("@CDAID", item.CDAID);
                            AddParam("@CROPID", item.CROPID);
                            AddParam("@SHSDATE", item.SHSDATE);
                            AddParam("@SHAID", item.SHAID);
                            AddParam("@VEHNO", item.VEHNO);
                            AddParam("@PACKER", item.PACKER);
                            AddParam("@WORKER", item.WORKER);
                            AddParam("@SHDAID", item.SHDAID);
                            AddParam("@PSIZE", item.PSIZE);
                            AddParam("@PQUNTITY", item.PQUNTITY);
                            AddParam("@CROPINVERT", item.CROPINVERT);
                            AddParam("@CRATECOUNT", item.CRATECOUNT);
                            AddParam("@COMPANYID", item.COMPANYID);
                            AddParam("@DEPTID", item.DEPTID);
                            AddParam("@WEIGHT", item.WEIGHT);
                            AddParam("@RATE", item.RATE);
                            AddParam("@ISDELETED", item.ISDELETED);
                            AddParam("@ISCOMPLETED", item.ISCOMPLETED);
                            AddParam("@CGRADE", item.CGRADE);
                            AddParam("@VDAID", item.VDAID);

                            using var reader2 = await command.ExecuteReaderAsync();
                            while (await reader2.ReadAsync())
                            {
                                result.Add(new ApiResponseMessage
                                {
                                    IsSuccessful = reader2.GetInt32(reader2.GetOrdinal("IsSuccessful")),
                                    ResponseCode = reader2.GetString(reader2.GetOrdinal("ResponseCode")),
                                    ResponseMessage = reader2.GetString(reader2.GetOrdinal("ResponseMessage")),
                                    ResponseValues = reader2.GetString(reader2.GetOrdinal("ResponseValues"))
                                });
                            }
                        }
                    }
                }

                // ---------- Transactions Call ----------
                var transactionItem = input.AllTransections?.FirstOrDefault();
                if (transactionItem != null)
                {
                    using (var command2 = connection.CreateCommand())
                    {
                        command2.Transaction = transaction;
                        command2.CommandText = "[SP_AddUpdAllTransections]";
                        command2.CommandType = CommandType.StoredProcedure;

                        void AddParam(string name, object value)
                        {
                            var p = command2.CreateParameter();
                            p.ParameterName = name;
                            p.Value = value ?? DBNull.Value;
                            command2.Parameters.Add(p);
                        }

                        AddParam("@TRAID", transactionItem.TRAID);
                        AddParam("@TRNSR", transactionItem.TRNSR);
                        AddParam("@TRNSID", transactionItem.TRNSID);
                        AddParam("@TRNTID", transactionItem.TRNTID);
                        AddParam("@TRNDT", transactionItem.TRNDT);
                        AddParam("@REFAID", transactionItem.REFAID);
                        AddParam("@MAID", transactionItem.MAID);
                        AddParam("@PAMT", transactionItem.PAMT);
                        AddParam("@CBAL", transactionItem.CBAL);
                        AddParam("@ADVAMT", transactionItem.ADVAMT);
                        AddParam("@ASID", transactionItem.ASID);
                        AddParam("@ISDELETED", transactionItem.ISDELETED);
                        AddParam("@COMPANYID", transactionItem.COMPANYID);
                        AddParam("@DEPTID", transactionItem.DEPTID);
                        AddParam("@CROPID", transactionItem.CROPID);

                        using var reader2 = await command2.ExecuteReaderAsync();
                        while (await reader2.ReadAsync())
                        {
                            result.Add(new ApiResponseMessage
                            {
                                IsSuccessful = reader2.GetInt32(reader2.GetOrdinal("IsSuccessful")),
                                ResponseCode = reader2.GetString(reader2.GetOrdinal("ResponseCode")),
                                ResponseMessage = reader2.GetString(reader2.GetOrdinal("ResponseMessage")),
                                ResponseValues = reader2.GetString(reader2.GetOrdinal("ResponseValues"))
                            });
                        }
                    }
                }

                // ---------- Stock Call ----------
                var stockItem = input.StockDetails?.FirstOrDefault();
                if (stockItem != null)
                {
                    using (var command3 = connection.CreateCommand())
                    {
                        command3.Transaction = transaction;
                        command3.CommandText = "[SP_AddUpdStockDetails]";
                        command3.CommandType = CommandType.StoredProcedure;

                        void AddParam(string name, object value)
                        {
                            var p = command3.CreateParameter();
                            p.ParameterName = name;
                            p.Value = value ?? DBNull.Value;
                            command3.Parameters.Add(p);
                        }

                        AddParam("@STAID", stockItem.STAID);
                        AddParam("@TRNDT", stockItem.TRNDT);
                        AddParam("@CROP", stockItem.CROP);
                        AddParam("@FAID", stockItem.FAID);
                        AddParam("@CIAID", stockItem.CIAID);
                        AddParam("@TOTCR", stockItem.TOTCR);
                        AddParam("@TOTWT", stockItem.TOTWT);
                        AddParam("@TRNTID", stockItem.TRNTID);
                        AddParam("@COMPANYID", stockItem.COMPANYID);
                        AddParam("@DEPTID", stockItem.DEPTID);
                        AddParam("@ISDELETED", stockItem.ISDELETED);
                        AddParam("@CGRADE", stockItem.CGRADE);
                        AddParam("@REFF", stockItem.REFF);
                        AddParam("@RTOTCR", stockItem.RTOTCR);

                        using var reader2 = await command3.ExecuteReaderAsync();
                        while (await reader2.ReadAsync())
                        {
                            result.Add(new ApiResponseMessage
                            {
                                IsSuccessful = reader2.GetInt32(reader2.GetOrdinal("IsSuccessful")),
                                ResponseCode = reader2.GetString(reader2.GetOrdinal("ResponseCode")),
                                ResponseMessage = reader2.GetString(reader2.GetOrdinal("ResponseMessage")),
                                ResponseValues = reader2.GetString(reader2.GetOrdinal("ResponseValues"))
                            });
                        }
                    }
                }

                // Commit transaction
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                result.Add(new ApiResponseMessage
                {
                    IsSuccessful = 0,
                    ResponseCode = "ERROR",
                    ResponseMessage = ex.Message,
                    ResponseValues = ""
                });
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return result;
        }


    }

}