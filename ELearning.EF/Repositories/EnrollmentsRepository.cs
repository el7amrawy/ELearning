using System.Linq.Expressions;
using AutoMapper;
using ELearning.Core.Interfaces.Repositories;
using ELearning.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearning.EF.Repositories
{
    public class EnrollmentsRepository : BaseRepository<Enrollment>, IEnrollmentsRepository
    {
        private readonly AppDbContext _context;
        public EnrollmentsRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Enrollment, bool>> predicate)
        {
            return await _context.Enrollments.AnyAsync(predicate);
        }
    }
}