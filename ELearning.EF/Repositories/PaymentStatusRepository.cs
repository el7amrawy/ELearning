using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;

namespace ELearning.EF.Repositories
{
    public class PaymentStatusRepository : BaseRepository<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}