using Microsoft.EntityFrameworkCore;
using Project.Model.Domain;
namespace Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<_GET_AccessRights_> AccessRights { get; set; }
        public DbSet<Implications> ST_Implication { get; set; }
        public DbSet<TblMailTemp> tblMailTemp { get; set; }
        public DbSet<GET_BOMPacking> tblBOMPackingStockConversion { get; set; }

        public DbSet<uploadPdf> uploadPdfs { get; set; }



        //public DbSet<MailTemplate> tblMailTemp { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MailTemplate>().HasNoKey(); // Since it's a raw SQL query
        //}
    }
}
