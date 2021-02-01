using JSL.CadCaminhoneiro.Core.DomainObjects;
using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class ModeloCaminhao : Entity
    {
        public ModeloCaminhao(
            string descricao, 
            string ano,
            Guid marcaCaminhaoId)
        {
            Descricao = descricao;
            Ano = ano;
            MarcaCaminhaoId = marcaCaminhaoId;
        }
        
        protected ModeloCaminhao() { }
        public string Descricao { get; private set; }
        public string Ano { get; private set; }
        public Guid MarcaCaminhaoId { get; private set; }
        public MarcaCaminhao MarcaCaminhao { get; private set; }

        public void Incluir(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }

        public void Alterar(
            string descricao, 
            string ano,
            Guid marcaCaminhaoId,
            DateTime? dataAlteracao)
        {
            Descricao = descricao;
            Ano = ano;
            MarcaCaminhaoId = marcaCaminhaoId;
            DataAlteracao = dataAlteracao;
        }
    }
}
