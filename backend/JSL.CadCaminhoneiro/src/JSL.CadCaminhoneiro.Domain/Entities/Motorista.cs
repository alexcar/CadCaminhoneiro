using JSL.CadCaminhoneiro.Core.DomainObjects;
using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class Motorista : Entity
    {
        public Motorista(
            string nome,
            string cpf,
            DateTime dataNascimento,
            string nomePai,
            string nomeMae,
            string naturalidade,
            string numeroRegistroGeral,
            string orgaoExpedicaoRegistroGeral,
            DateTime dataExpedicaoRegistroGeral,
            Endereco endereco,
            Habilitacao habilitacao,
            Caminhao caminhao)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            NomePai = nomePai;
            NomeMae = nomeMae;
            Naturalidade = naturalidade;
            NumeroRegistroGeral = numeroRegistroGeral;
            OrgaoExpedicaoRegistroGeral = orgaoExpedicaoRegistroGeral;
            DataExpedicaoRegistroGeral = dataExpedicaoRegistroGeral;
            Endereco = endereco;
            Habilitacao = habilitacao;
            Caminhao = caminhao;
        }
        
        protected Motorista() { }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string NomePai { get; private set; }
        public string NomeMae { get; private set; }
        public string Naturalidade { get; private set; }
        public string NumeroRegistroGeral { get; private set; }
        public string OrgaoExpedicaoRegistroGeral { get; private set; }
        public DateTime DataExpedicaoRegistroGeral { get; private set; }
        
        public Endereco Endereco { get; private set; }
        public Habilitacao Habilitacao { get; private set; }
        public Caminhao Caminhao { get; private set; }

        public void Incluir(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }

        public void Alterar(
            string nome,
            string cpf,
            DateTime dataNascimento,
            string nomePai,
            string nomeMae,
            string naturalidade,
            string numeroRegistroGeral,
            string orgaoExpedicaoRegistroGeral,
            DateTime dataExpedicaoRegistroGeral,
            Endereco endereco,
            Habilitacao habilitacao,
            Caminhao caminhao,
            DateTime? dataAlteracao)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            NomePai = nomePai;
            NomeMae = nomeMae;
            Naturalidade = naturalidade;
            NumeroRegistroGeral = numeroRegistroGeral;
            OrgaoExpedicaoRegistroGeral = orgaoExpedicaoRegistroGeral;
            DataExpedicaoRegistroGeral = dataExpedicaoRegistroGeral;
            Endereco = endereco;
            Habilitacao = habilitacao;
            Caminhao = caminhao;
            DataAlteracao = dataAlteracao;
        }
    }
}
