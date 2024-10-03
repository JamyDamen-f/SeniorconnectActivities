using Microsoft.EntityFrameworkCore;

namespace SeniorConnectActivities.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
               
        }
    }
}
