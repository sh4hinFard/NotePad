using Microsoft.EntityFrameworkCore;
using Be;
using Microsoft.Extensions.Configuration;

namespace Dal
{
    public class Database : DbContext
    {

        public Database()
        {

        }       
        public Database(DbContextOptions<Database> options)
        : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Notebook> notebooks { get; set; }
        public DbSet<Task_Todo> task_Todos { get; set; }
        public DbSet<Logs> logs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: true, reloadOnChange: false)
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                Console.WriteLine($"Connection string: {connectionString}");
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NoteBook;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }

}
