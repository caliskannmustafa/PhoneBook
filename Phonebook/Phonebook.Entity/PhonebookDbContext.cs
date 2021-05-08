using System;

namespace Phonebook.Entity
{
    public class PhonebookDbContext
    {
        public readonly IConfiguration Configuration;

        public FirstDemoDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseNpgsql(Configuration.GetConnectionString("DemoDBConnection"));
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
