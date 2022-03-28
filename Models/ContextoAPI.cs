using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DesejosWebAPI.Models
{
    public class ContextoAPI : DbContext
    {
        public static IConfigurationRoot Configuration
        {
            get;
            set;
        }

        public DbSet<Usuario> Usuario
        {
            get;
            set;
        }

        public DbSet<Desejo> Desejo
        {
            get;
            set;
        }

        public ContextoAPI(DbContextOptions<ContextoAPI> options) : base(options)
        {

        }
    }
}
