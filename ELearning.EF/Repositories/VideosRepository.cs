using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class VideosRepository : BaseRepository<Video>, IVideosRepository
    {
        public VideosRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}