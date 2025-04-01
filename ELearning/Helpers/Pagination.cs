namespace ELearning.Helpers
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public Pagination(int pageNumber, int pageSize, int count)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Count = count;
            Pages = DivideAndRoundUp(count, pageSize);
        }
        private int DivideAndRoundUp(int a, int b) => (a + b - 1) / b;
    }
}