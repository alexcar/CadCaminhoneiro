using System.Linq;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data.Repository.Extensions
{
    public static class MotoristaQueryableApplyFilterExtension
    {
        public static IQueryable<MotoristaListDto> ApplyFilter(
            this IQueryable<MotoristaListDto> source, string filter)
        {
            if (filter == null)
                return source;

            var filterList = filter.Split(',');
            var predicate = PredicateBuilder.False<MotoristaListDto>();

            foreach (var item in filterList)
            {
                var temp = item;
                predicate = predicate.Or(p => p.Nome.Contains(temp));
            }

            return source.Where(predicate);
        }
    }
}
