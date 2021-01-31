using System.Linq;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data.Repository.QueryObjects
{
    public static class MarcaCaminhaoListDtoSelect
    {
        public static IQueryable<MarcaCaminhaoListDto> MapMarcaCaminhaoToDto(this IQueryable<MarcaCaminhao> marcasCaminhao)
        {
            return marcasCaminhao.Select(p => new MarcaCaminhaoListDto
            {
                Id = p.Id,
                Descricao = p.Descricao
            });
        }
    }
}
