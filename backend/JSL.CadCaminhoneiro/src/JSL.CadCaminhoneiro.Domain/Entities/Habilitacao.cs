using JSL.CadCaminhoneiro.Core.DomainObjects;
using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class Habilitacao : Entity
    {
        public Habilitacao(
            string numeroRegistro,
            string categoria,
            DateTime dataPrimeiraHabilitacao,
            DateTime dataValidade,
            DateTime dataEmissao,
            string observacao,
            Guid motoristaId)
        {
            NumeroRegistro = numeroRegistro;
            Categoria = categoria;
            DataPrimeiraHabilitacao = dataPrimeiraHabilitacao;
            DataValidade = dataValidade;
            DataEmissao = dataEmissao;
            Observacao = observacao;
            MotoristaId = motoristaId;
        }
        
        protected Habilitacao() { }
        public string NumeroRegistro { get; private set; }
        public string Categoria { get; private set; }
        public DateTime DataPrimeiraHabilitacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public string Observacao { get; private set; }
        
        public Guid MotoristaId { get; private set; }
        public Motorista Motorista { get; private set; }

        public void Incluir(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }

        public void Alterar(
            string numeroRegistro,
            string categoria,
            DateTime dataPrimeiraHabilitacao,
            DateTime dataValidade,
            DateTime dataEmissao,
            string observacao,
            DateTime? dataAlteracao
            )
        {
            NumeroRegistro = numeroRegistro;
            Categoria = categoria;
            DataPrimeiraHabilitacao = dataPrimeiraHabilitacao;
            DataValidade = dataValidade;
            DataEmissao = dataEmissao;
            Observacao = observacao;
            DataAlteracao = dataAlteracao;
        }
    }
}
