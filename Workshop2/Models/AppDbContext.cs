using Microsoft.EntityFrameworkCore;

namespace Workshop2.Models
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }
    }
}
