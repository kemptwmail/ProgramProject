using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Breakfast> FavoriteBreakfastFoods { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
