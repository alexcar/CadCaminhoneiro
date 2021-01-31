using System.Linq;

namespace JSL.CadCaminhoneiro.Data.Repository.Extensions
{
    public static class QueryableApplyPageExtension
    {
        public static IQueryable<T> ApplyPage<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            return source.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
        }
    }
}
