using Microsoft.AspNetCore.Http;

namespace ELearning.Core.DTOs
{
    public class EditLectureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public IFormFile? VideoFile { get; set; }
    }
}