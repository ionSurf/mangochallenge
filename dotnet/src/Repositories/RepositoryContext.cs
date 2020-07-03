using Microsoft.EntityFrameworkCore;

namespace Entities.Models {
    public class RepositoryContext : DbContext {
        public RepositoryContext( DbContextOptions options )
            : base( options ) {}
        public DbSet<Portrait> Portraits {get;set;}
        public DbSet<User> Users {get;set;}
    }
}