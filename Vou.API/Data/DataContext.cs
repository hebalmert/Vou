using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vou.Shared.Entities;
using Vou.Shared.EntitiesSoft;

namespace Vou.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //Modelos del Entities
        public DbSet<City> Cities => Set<City>();

        public DbSet<Country> Countries => Set<Country>();

        public DbSet<State> States => Set<State>();

        public DbSet<SoftPlan> SoftPlans => Set<SoftPlan>();

        public DbSet<Manager> Managers => Set<Manager>();

        public DbSet<Corporate> Corporates => Set<Corporate>();



        //Modelos del Software
        public DbSet<Register> Registers=> Set<Register>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Para tomar los calores de ConfigEntities
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //De esta manera podemos crear un regla general para un tipo de Dato
            //sera automatico.
            //configurationBuilder.Properties<string>().HaveMaxLength(50);
        }
    }
}
