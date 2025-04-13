namespace ELearning.Helpers
{
    public class Pagination(int pageNumber, int pageSize, int count)
    {
        public int PageNumber => pageNumber;
        public int PageSize => pageSize;
        public int Count => count;
        public int Pages => DivideAndRoundUp(Count, PageSize);
        public bool HasNext => PageNumber < Pages;
        public bool HasPrevious => PageNumber > 1;
        public IEnumerable<int> NearbyPages => Enumerable
        .Range(Math.Max(1, PageNumber - 2), Math.Min(5, Pages))
        .Where(p => p >= 1 && p <= Pages);
        private int DivideAndRoundUp(int a, int b) => (a + b - 1) / b;
    }
}