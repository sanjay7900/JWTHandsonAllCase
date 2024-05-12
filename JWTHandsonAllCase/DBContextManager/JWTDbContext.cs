using JWTHandsonAllCase.Models.DTOModel;
using JWTHandsonAllCase.Models.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace JWTHandsonAllCase.DBContextManager
{
    public class JWTDbContext:DbContext
    {
        public DbSet<UserModel> Users { get; set; } 
        public DbSet<RevokedToken> RevokedTokens { get; set; }  
        public JWTDbContext(DbContextOptions<JWTDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasIndex(e => new { e.Email ,e.RefreshToken})
                .IsUnique();

          
        }
    }
}
