using Microsoft.EntityFrameworkCore;

namespace MarketerAPI.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }


        public virtual DbSet<Offer> Offers { get; set; }

     
    }
}
