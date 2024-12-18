using ELearning.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF
{
	public class AppDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
		public DbSet<Student> Students { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Lecture> Lectures { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
			builder.Entity<Student>().HasMany(s => s.Courses).WithMany(c =>c.Students).UsingEntity<Enrollment>();
			base.OnModelCreating(builder);
		}
    }
}
