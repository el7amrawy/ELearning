using Microsoft.AspNetCore.Http;

namespace ELearning.Core.DTOs
{
    public class CreateLectureDto
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string? Notes { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}