namespace ProjectPrn221.Pages.Common
{
    public class Utils
    {
        public static (IQueryable<T> query, decimal totalPage) Page<T>(IQueryable<T> en, int pageSize, int page)
        {
            decimal totalPage = Math.Ceiling((decimal)en.Count() / pageSize);
            page = PageLimit(page);
            return (en.Skip(page * pageSize).Take(pageSize), totalPage);
        }

        public static int PageLimit(int pageNum)
        {
            pageNum -= 1;
            if (pageNum <= 0)
            {
                pageNum = 0;
            }
            return pageNum;
        }
    }
}
