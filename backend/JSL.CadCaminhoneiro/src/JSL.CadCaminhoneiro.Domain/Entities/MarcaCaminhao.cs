using JSL.CadCaminhoneiro.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class MarcaCaminhao : Entity
    {
        public MarcaCaminhao(string descricao)
        {
            Descricao = descricao;
        }

        protected MarcaCaminhao() { }
        public string Descricao { get; private set; }
        public ICollection<ModeloCaminhao> ModelosCaminhao { get; private set; }

        public void Incluir(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }

        public void Alterar(string descricao, DateTime? dataAlteracao)
        {
            Descricao = descricao;
            DataAlteracao = dataAlteracao;
        }
    }
}
