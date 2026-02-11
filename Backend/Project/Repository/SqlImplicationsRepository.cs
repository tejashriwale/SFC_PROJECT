using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlImplicationsRepository : IImplicationsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlImplicationsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Implications>> GetAllAsync()
        {
            string sql = "SELECT * FROM getImplications('%')";

            return await dbContext.ST_Implication.FromSqlRaw<Implications>(sql).ToListAsync();
        }

        public async Task<List<Implications>> GetByGroupAsync(string groupid)
        {
            string sql = "SELECT * FROM getImplications('" + groupid + "')";

            return await dbContext.ST_Implication.FromSqlRaw<Implications>(sql).ToListAsync();
        }
    }
}

