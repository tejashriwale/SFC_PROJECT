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
                command.CommandText = "SELECT Top 20 * FROM [GET_SabhasadMaster]('" + input.SRAID + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "SELECT  * FROM [GET_SabhasadMaster]('" + input.SRAID + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
