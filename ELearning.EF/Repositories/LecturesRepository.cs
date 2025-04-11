using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class LecturesRepository : BaseRepository<Lecture>, ILecturesRepository
    {
        public LecturesRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}