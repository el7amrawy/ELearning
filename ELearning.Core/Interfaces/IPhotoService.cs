using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ELearning.Core.Interfaces
{
    public interface IPhotoService
    {
        public Task<ImageUploadResult> AddPhotoAsync(IFormFile file, int h = 0, int w = 0);
        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
