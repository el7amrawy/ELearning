using ELearning.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF
{
	public class AppDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Lecture> Lectures { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

			//builder.Entity<AppUser>().HasMany(s => s.Courses).WithMany(c =>c.Students).UsingEntity<Enrollment>();
			builder.Entity<AppUser>().HasIndex(u => u.ImageId).IsUnique().HasFilter("[ImageId] IS NOT NULL");
			builder.Entity<Course>().HasIndex(u => u.ImageId).IsUnique().HasFilter("[ImageId] IS NOT NULL");
			builder.Entity<Section>().HasIndex(u => u.Order).IsUnique();

			base.OnModelCreating(builder);
		}
    }
}