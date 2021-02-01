using System.Linq;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data.Repository.Extensions
{
    public static class ModeloCaminhaoQueryableApplyFilterExtension
    {
        public static IQueryable<ModeloCaminhaoListDto> ApplyFilter(
            this IQueryable<ModeloCaminhaoListDto> source, string filter)
        {
            if (filter == null)
                return source;

            var filterList = filter.Split(',');
            var predicate = PredicateBuilder.False<ModeloCaminhaoListDto>();

            foreach (var item in filterList)
            {
                var temp = item;
                predicate = predicate.Or(p => p.Descricao.Contains(temp));
            }

            return source.Where(predicate);
        }
    }
}
