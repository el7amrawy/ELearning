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
            _cloudinary = new Cloudinary(_settings.URL)
            {
                Api = { Timeout = 900000 }
            };
        }
        public async Task<VideoUploadResult> AddVideoAsync(IFormFile videoFile,string instructorName,int courseId)
        {
            if (videoFile == null || videoFile.Length < 0)
                throw new ArgumentNullException(nameof(videoFile));

            using var fileStream = videoFile.OpenReadStream();

            if (videoFile.Length > 100 * 1024 * 1024) // 100MB in bytes
                throw new InvalidOperationException("Video exceeds max size limit (100MB).");

            var eagerTransforms = new List<Transformation>
            {
                new Transformation().Width(1280).Height(720).Crop("limit"),
            };


            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(videoFile.Name, fileStream),
                Folder = $"{_settings.Folder}/Courses/{instructorName}/{courseId}",
                Format = "mp4",
                EagerTransforms = eagerTransforms, // Apply transformations
                EagerAsync = true,
            };

            return await _cloudinary.UploadLargeAsync(uploadParams);
        }

        public async Task<DeletionResult> DeleteVideoAsync(string publicId)
        {
            return await _cloudinary.DestroyAsync(new DeletionParams(publicId) { ResourceType = ResourceType.Video });
        }
    }
}