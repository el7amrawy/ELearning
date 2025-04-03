using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class LanguagesRepository(AppDbContext context, IMapper mapper) : BaseRepository<Language>(context, mapper),ILanguagesRepository
    {
    }
}