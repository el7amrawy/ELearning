using ELearning.Core.Interfaces;
using ELearning.Core.Models;

namespace ELearning.EF
{
    public class Seed
    {
        private readonly IUnitOfWork _unitOfOfWork;
        public Seed(IUnitOfWork unitOfWork)
        {
            _unitOfOfWork = unitOfWork;   
        }
        private async Task SeedLanguages()
        {
            if (await _unitOfOfWork.Languages.CountAsync() > 0)
                return;

            await _unitOfOfWork.Languages.AddRangeAsync(
            [
                new Language { Name="English"},
                new Language { Name="Arabic"}
            ]);
        }
        private async Task SeedLevels()
        {
            if (await _unitOfOfWork.Levels.CountAsync() > 0)
                return;

            await _unitOfOfWork.Levels.AddRangeAsync(new List<Level>
            {
                new Level{Name="Beginner"},
                new Level{Name="Medium"},
                new Level{Name="Advanced"}
            });
        }
        private async Task SeedCourseStatus()
        {
            if (await _unitOfOfWork.CoursesStatus.CountAsync() > 0)
                    return;

            await _unitOfOfWork.CoursesStatus.AddRangeAsync(
            [
                new CourseStatus{Name="Draft"},
                new CourseStatus{Name="Available"},
                new CourseStatus{Name="Unavailable"},
            ]);
        }
        public async Task SeedAsync()
        {
            await SeedCourseStatus();
            await SeedLanguages();
            await SeedLevels();

            await _unitOfOfWork.CompleteAsync();
        }
    }
}
