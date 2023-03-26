using EFC_.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFC_.Contexts
{
    internal class DataContext : DbContext
    {

        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nikol\source\repos\EFC-sql\EFC_\Contexts\sql_db.mdf;Integrated Security=True;Connect Timeout=30";

        #region constructors
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #endregion

        #region overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion




        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<CaseEntity> Cases { get; set; } = null!;




    }
}
