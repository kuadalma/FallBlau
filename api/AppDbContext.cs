using Microsoft.EntityFrameworkCore;
using api.Models;
namespace api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
        : base(options) { _configuration = configuration; }

        public IConfiguration _configuration { get; }
        public DbSet<User> user { get; set; }
        public DbSet<Models.Task> task { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(_configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(_configuration.GetConnectionString("MySqlConnection"))));
            services.AddDbContext<AppDbContext>();
        }
    }
}
