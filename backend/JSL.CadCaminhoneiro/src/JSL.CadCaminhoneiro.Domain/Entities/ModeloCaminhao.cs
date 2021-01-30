using JSL.CadCaminhoneiro.Core.DomainObjects;
using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class ModeloCaminhao : Entity
    {
        public ModeloCaminhao(
            string descricao, 
            string ano,
            MarcaCaminhao marcaCaminhao)
        {
            Descricao = descricao;
            Ano = ano;
            MarcaCaminhao = marcaCaminhao;
        }
        
        protected ModeloCaminhao() { }
        public string Descricao { get; private set; }
        public string Ano { get; private set; }
        public MarcaCaminhao MarcaCaminhao { get; private set; }

        public void Incluir(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }

        public void Alterar(
            string descricao, 
            string ano,
            MarcaCaminhao marcaCaminhao,
            DateTime? dataAlteracao)
        {
            Descricao = descricao;
            Ano = ano;
            MarcaCaminhao = marcaCaminhao;
            DataAlteracao = dataAlteracao;
        }
    }
}
