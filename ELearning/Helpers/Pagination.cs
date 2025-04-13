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
        private int DivideAndRoundUp(int a, int b) => (a + b - 1) / b;
    }
}