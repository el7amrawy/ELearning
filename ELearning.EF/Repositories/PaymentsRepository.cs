using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class PaymentsRepository : BaseRepository<Payment>, IPaymentsRepository
    {
        public PaymentsRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}