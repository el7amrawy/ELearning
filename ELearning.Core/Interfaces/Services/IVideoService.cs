using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ELearning.Core.Interfaces.Services
{
    public interface IVideoService
    {
        public Task<VideoUploadResult> AddVideoAsync(IFormFile videoFile, string instructorName, int courseId);
        public Task<DeletionResult> DeleteVideoAsync(string publicId);
    }
}