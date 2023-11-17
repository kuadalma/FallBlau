using Microsoft.EntityFrameworkCore;
using api.Models;
namespace api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options) { _configuration = configuration; }

        public IConfiguration _configuration { get; }
        public DbSet<User> User { get; set; }
        public DbSet<Models.Task> Task { get; set; }
        public void ConfigureServices(IServiceCollection services ,DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(_configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(_configuration.GetConnectionString("MySqlConnection"))));
            dbContextOptionsBuilder.UseLazyLoadingProxies();
        }
        public void OnModelCreating(ModelBuilder optionsbuilder)
        {
            base.OnModelCreating(optionsbuilder);
        }
    }
}
