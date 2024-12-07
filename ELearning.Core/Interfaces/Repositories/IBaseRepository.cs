namespace ELearning.Core.Interfaces.Repositories
{
	public interface IBaseRepository<Model> where Model : class
	{
		public Task<IEnumerable<Model>> GetAllAsync();
	}
}
