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
        public async Task<ServiceResult> SwapOrderAsync(int sectionId, int lectureId1, int lectureId2)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var lec1 = await _unitOfWork.Lectures.GetItemAsync(l => l.SectionId == sectionId && l.Id == lectureId1);
                var lec2 = await _unitOfWork.Lectures.GetItemAsync(l => l.SectionId == sectionId && l.Id == lectureId2);

                if (lec1 == null || lec2 == null) return ServiceResult.Failure($"lecture does not exist");

                var tempOrder = lec1.Order;

                lec1.Order = -1;
                await _unitOfWork.CompleteAsync();

                lec1.Order = lec2.Order;
                lec2.Order = tempOrder;

                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();

                return ServiceResult.Success();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ServiceResult.Failure("problem swapping lectures");
            }            
        }
        public async Task<ServiceResult> DeleteAsync(int lectureId, int sectionId)
        {
            var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var lec = await _unitOfWork.Lectures
               .GetItemAsync(l => l.SectionId == sectionId && l.Id == lectureId, ["Video", "Material"]);

                if (lec == null) return ServiceResult.Failure("lecture does not exist");

                _unitOfWork.Lectures.Delete(lec);

                if (lec.Video != null) { 
                    var vidRes = await _videoService.DeleteVideoAsync(lec.Video.PublicId);

                    if (vidRes.Error != null) return ServiceResult.Failure(vidRes.Error.Message);

                    _unitOfWork.Videos.Delete(lec.Video);
                }

                // material deletion logic

                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();

                return ServiceResult.Success();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ServiceResult.Failure("failed to delete lecture");
            }
           
        }
    }
}