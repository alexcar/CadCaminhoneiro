using System.Linq;
using System.Linq.Dynamic.Core;

namespace JSL.CadCaminhoneiro.Data.Repository
{
    public static class QueryableApplySortExtension
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
        {

            // https://github.com/itorian/Sorting-in-WebAPI
            //if (source == null)
            //{
            //    throw new ArgumentNullException("source");
            //}

            if (sort == null)
            {
                return source;
            }

            var lstSort = sort.Split(',');

            var sortExpression = string.Empty;

            foreach (var sortOption in lstSort)
            {
                if (sortOption.StartsWith("-"))
                {
                    sortExpression = sortExpression + sortOption.Remove(0, 1) + " descending,";
                }
                else
                {
                    sortExpression = sortExpression + sortOption + ",";
                }
            }

            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                // Note: system.linq.dynamic NuGet package is required here to operate OrderBy on string
                source = source.OrderBy(sortExpression.Remove(sortExpression.Count() - 1));
            }

            return source;
        }
    }
}
