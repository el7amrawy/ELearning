using ELearning.Core.Common;
using ELearning.Core.DTOs;

namespace ELearning.Core.Interfaces.Services
{
    public interface ISectionService
    {
        public Task<ServiceResult> CreateAsync(SectionDto section);
        public Task<ServiceResult> SwapOrder(int courseId, int sectionId1, int sectionId2);
    }
}