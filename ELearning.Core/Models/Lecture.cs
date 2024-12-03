using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
	public class Lecture
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string VideoURL {  get; set; } = null!;
		[Required,ForeignKey(nameof(Section))]
		public int SectionId { get; set; }
		public Section? Section { get; set; }
		public virtual Material? Material { get; set; }
	}
}
