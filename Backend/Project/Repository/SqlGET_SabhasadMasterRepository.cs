using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SabhasadMasterRepository : IGET_SabhasadMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SabhasadMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SabhasadMaster>> getasync(GET_SabhasadMasterDomain input)
        {
            List<GET_SabhasadMaster> list = new List<GET_SabhasadMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_SabhasadMaster](@p0 ,@p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SabhasadMaster
                        {
                            AGE = reader.GetInt32("AGE"),
                            SRAID = reader.GetString("SRAID"),
                            SABHASADNAME = reader.GetString("SABHASADNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            BIRTHDATE = reader.GetString("BIRTHDATE"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            PINCODE = reader.GetInt32("PINCODE"),
                            CITY = reader.GetString("CITY"),
                            SECOUNDARYBUSINES = reader.GetString("SECOUNDARYBUSINES"),
                            IRRIGATEDLAND = reader.GetString("IRRIGATEDLAND"),
                            STATE = reader.GetString("STATE"),
                            PHOTOS = reader.GetString("PHOTOS"),
                            NOMINALAGE = reader.GetInt32("NOMINALAGE"),
                            NOMINALNAME = reader.GetString("NOMINALNAME"),
                            NOMINALRELATION = reader.GetString("NOMINALRELATION"),
                            PRIMARYBUSINESS = reader.GetString("PRIMARYBUSINESS"),
                            TOTALLAND = reader.GetString("TOTALLAND"),
                            NONIRRIGATEDLAND = reader.GetString("NONIRRIGATEDLAND"),
                            FORM = reader.GetString("FORM"),
                            ImplicationTitle = reader.GetString("ImplicationTitle"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            REGDATE = reader.GetString("REGDATE"),
                            REGNO = reader.GetString("REGNO"),




                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_SabhasadMaster>> getkeyword(GET_SabhasadMasterDomain input)
        {
            List<GET_SabhasadMaster> list = new List<GET_SabhasadMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SabhasadMaster](@p0 ,@p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SabhasadMaster
                        {
                            AGE = reader.GetInt32("AGE"),
                            SRAID = reader.GetString("SRAID"),
                            SABHASADNAME = reader.GetString("SABHASADNAME"),
                            CONTACTNUMBER = reader.GetString("CONTACTNUMBER"),
                            BIRTHDATE = reader.GetString("BIRTHDATE"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            PINCODE = reader.GetInt32("PINCODE"),
                            CITY = reader.GetString("CITY"),
                            SECOUNDARYBUSINES = reader.GetString("SECOUNDARYBUSINES"),
                            IRRIGATEDLAND = reader.GetString("IRRIGATEDLAND"),
                            STATE = reader.GetString("STATE"),
                            PHOTOS = reader.GetString("PHOTOS"),
                            NOMINALAGE = reader.GetInt32("NOMINALAGE"),
                            NOMINALNAME = reader.GetString("NOMINALNAME"),
                            NOMINALRELATION = reader.GetString("NOMINALRELATION"),
                            PRIMARYBUSINESS = reader.GetString("PRIMARYBUSINESS"),
                            TOTALLAND = reader.GetString("TOTALLAND"),
                            NONIRRIGATEDLAND = reader.GetString("NONIRRIGATEDLAND"),
                            FORM = reader.GetString("FORM"),
                            ImplicationTitle = reader.GetString("ImplicationTitle"),
                            SDISTRICT = reader.GetString("SDISTRICT"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            REGDATE = reader .GetString("REGDATE"),
                            REGNO = reader.GetString("REGNO"),



                        });
                    }
                }
            }
            return list;
        }
    }
}
