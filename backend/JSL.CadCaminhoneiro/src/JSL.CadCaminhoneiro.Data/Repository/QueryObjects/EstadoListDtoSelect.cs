using System.Linq;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data.Repository.QueryObjects
{
    public static class EstadoListDtoSelect
    {
        public static IQueryable<EstadoListDto> MapEstadoToDto(this IQueryable<Estado> estados)
        {
            return estados.Select(p => new EstadoListDto
            {
                Uf = p.Uf,
                Descricao = p.Descricao
            });
        }
    }
}
