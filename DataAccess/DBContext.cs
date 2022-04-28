using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
            _users = new UserRepository(this);
        }

        private UserRepository _users;
        public UserRepository UserRepository()
        {
            return _users;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>().HasKey(p => new { p.ID });
            modelBuilder.Entity<User>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
