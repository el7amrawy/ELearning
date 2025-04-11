using AutoMapper;
using ELearning.Core.Common;
using ELearning.Core.DTOs;
using ELearning.Core.Interfaces;
using ELearning.Core.Interfaces.Services;
using ELearning.Core.Models;

namespace ELearning.Core.Services
{
    public class LectureService : ILectureService
    {
        private readonly IVideoService _videoService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LectureService(IVideoService videoService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _videoService = videoService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CreateAsync(CreateLectureDto lectureDto, string instructorName, int courseId)
        {
            try
            {
                var lecturesCount = await _unitOfWork.Lectures.CountAsync(l => l.SectionId == lectureDto.SectionId);

                var lecture = _mapper.Map<Lecture>(lectureDto);

                lecture.Order = lecturesCount + 1;

                var res = await _videoService.AddVideoAsync(lectureDto.VideoFile, instructorName, courseId);

                if (res.Error != null) return ServiceResult.Failure(res.Error.Message);

                var video = new Video { CreatedAt = DateTime.UtcNow, PublicId = res.PublicId, URL = res.SecureUrl.AbsoluteUri };

                lecture.Video = video;

                _unitOfWork.Lectures.Add(lecture);

                if (await _unitOfWork.CompleteAsync() < 1) return ServiceResult.Failure("problem creating lecture");

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure(ex.Message);
            }
        }
    }
}