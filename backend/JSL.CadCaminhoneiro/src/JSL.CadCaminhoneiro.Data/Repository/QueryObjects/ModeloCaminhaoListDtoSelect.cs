using System.Linq;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data.Repository.QueryObjects
{
    public static class ModeloCaminhaoListDtoSelect
    {
        public static IQueryable<ModeloCaminhaoListDto> MapModeloCaminhaoToDto(this IQueryable<ModeloCaminhao> modelosCaminhao)
        {
            return modelosCaminhao.Select(p => new ModeloCaminhaoListDto
            {
                Id = p.Id,
                Descricao = p.Descricao,
                Ano = p.Ano,
                MarcaCaminhaoListDto = new MarcaCaminhaoListDto
                { 
                    Id = p.MarcaCaminhao.Id,
                    Descricao = p.MarcaCaminhao.Descricao
                }
            });
        }
    }
}
