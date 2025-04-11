using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ELearning.Core.Helpers;
using ELearning.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ELearning.Core.Services
{
    public class VideoService : IVideoService
    {
        private readonly CloudinarySettings _settings;
        private readonly Cloudinary _cloudinary;
        public VideoService(IOptions<CloudinarySettings> options)
        {
            _settings = options.Value;
            _cloudinary = new Cloudinary(_settings.URL);
        }
        public async Task<VideoUploadResult> AddVideoAsync(IFormFile videoFile,string instructorName,int courseId)
        {
            if (videoFile == null || videoFile.Length < 0)
                throw new ArgumentNullException(nameof(videoFile));

            using var fileStream = videoFile.OpenReadStream();

            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(videoFile.Name, fileStream),
                Folder = $"{_settings.Folder}/Courses/{instructorName}/{courseId}",
                Format = "mp4"
            };

            return await _cloudinary.UploadLargeAsync(uploadParams);
        }

        public async Task<DeletionResult> DeleteVideoAsync(string publicId)
        {
            return await _cloudinary.DestroyAsync(new DeletionParams(publicId));
        }
    }
}