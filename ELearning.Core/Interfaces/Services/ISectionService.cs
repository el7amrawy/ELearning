using ELearning.Core.Common;
using ELearning.Core.DTOs;

namespace ELearning.Core.Interfaces.Services
{
    public interface ISectionService
    {
        public Task<ServiceResult> CreateAsync(SectionDto section);
    }
}