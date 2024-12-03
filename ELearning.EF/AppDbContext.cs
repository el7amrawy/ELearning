using ELearning.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF
{
	public class AppDbContext : IdentityDbContext<User,IdentityRole<int>,int>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
		public DbSet<Student> Students { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Lecture> Lectures { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<Section> Sections { get; set; }
	}
}
