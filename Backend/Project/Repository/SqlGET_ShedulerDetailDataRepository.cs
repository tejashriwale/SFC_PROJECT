using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_ShedulerDetailDataRepository : IGET_ShedulerDetailDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ShedulerDetailDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ShedulerDetailData>> getasync(GET_ShedulerDetailDataDomain input)
        {

            List<GET_ShedulerDetailData> list = new List<GET_ShedulerDetailData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ShedulerDetailData](@p0,@p1,@p2,@p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.VAID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ShedulerDetailData
                        {
                            DATE = reader.GetString("DATE"),
                            VAID = reader.GetString("VAID"),
                            PACKER = reader.GetString("PACKER"),
                            PACKERNAME = reader.GetString("PACKERNAME"),
                            WORKER = reader.GetString("WORKER"),
                            WORKERNAME = reader.GetString("WORKERNAME"),
                            VEHNO = reader.GetString("VEHNO"),
                            SIZE = reader.GetString("SIZE"),
                            PQUNTITY = reader.GetString("PQUNTITY"),
                            COUNT = reader.GetString("COUNT"),
                            PSIZE = reader.GetString("PSIZE"),
                            SHAID = reader.GetString("SHAID"),
                            SHDAID = reader.GetString("SHDAID")

                        });
                    }
                }
            }
            return list;
        }
    }
    
}
