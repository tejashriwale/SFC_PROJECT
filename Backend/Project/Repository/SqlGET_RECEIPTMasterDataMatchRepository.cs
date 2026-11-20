using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RECEIPTMasterDataMatchRepository : IGET_RECEIPTMasterDataMatchRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RECEIPTMasterDataMatchRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RECEIPTMasterDataMatch>> getasync(GET_RECEIPTMasterDataMatchDomain input)
        {
            List<GET_RECEIPTMasterDataMatch> list = new List<GET_RECEIPTMasterDataMatch>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_RECEIPTMasterDataMatch](@p0 , @p1 , @p2 , @p3 , @p4)order by CASHSTATUS,DATEO DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMasterDataMatch
                        {

                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            STATUS = reader.GetString("STATUS"),
                            MAID = reader.GetString("MAID"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            PKID = reader.GetString("PKID"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                            DATEO = reader.GetDateTime("DATEO"),
                            CASHSTATUS = reader.GetBoolean("CASHSTATUS"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE"),


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_RECEIPTMasterDataMatch>> getkeyword(GET_RECEIPTMasterDataMatchDomain input)
        {
            List<GET_RECEIPTMasterDataMatch> list = new List<GET_RECEIPTMasterDataMatch>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_RECEIPTMasterDataMatch](@p0 , @p1 , @p2 , @p3 , @p4)order by CASHSTATUS,DATEO DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTMasterDataMatch
                        {

                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            STATUS = reader.GetString("STATUS"),
                            MAID = reader.GetString("MAID"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),
                            FNAME = reader.GetString("FNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            PKID = reader.GetString("PKID"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                            DATEO = reader.GetDateTime("DATEO"),
                            CASHSTATUS = reader.GetBoolean("CASHSTATUS"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ISCLOSE = reader.GetBoolean("ISCLOSE"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
