using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
	public class Material
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = null!;
		[Required]
		public string Description { get; set; } = null!;
		[Required,ForeignKey(nameof(Lecture))]
		public int LectureId { get; set; }
		public Lecture? Lecture { get; set; }
	}
}
