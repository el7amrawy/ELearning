using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class CourseStatusRepository:BaseRepository<CourseStatus>,ICourseStatusRepository
    {
        public CourseStatusRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}