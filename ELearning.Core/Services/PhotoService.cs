using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ELearning.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using ELearning.Core.Helpers;
using Microsoft.Extensions.Options;

namespace ELearning.Core.Services
{
    public class PhotoService: IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;
        public PhotoService(IOptions<CloudinarySettings> options)
        {
            _cloudinarySettings = options.Value;
            _cloudinary = new Cloudinary(_cloudinarySettings.URL);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file, int h = 0, int w = 0)
        {
            if (file == null || file.Length < 0)
                throw new ArgumentNullException(nameof(file));

            using var fileStream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.Name, fileStream),
                Folder = _cloudinarySettings.Folder,
                Transformation = (h == 0 || w == 0) ? new Transformation() :
                    new Transformation().Height(h).Width(w).Crop("fill")
            };

            return await _cloudinary.UploadAsync(uploadParams);
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            return await _cloudinary.DestroyAsync(new DeletionParams(publicId));
        }
    }
}
