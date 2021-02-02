using JSL.CadCaminhoneiro.Core.DomainObjects;
using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class Endereco : Entity
    {
        public Endereco(
            string logradouro, 
            string numero, 
            string complemento,
            string bairro,
            string municipio,
            string uf,
            string cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Municipio = municipio;
            Uf = uf;
            Cep = cep;
        }
        
        protected Endereco() { }        
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Municipio { get; private set; }
        public string Uf { get; private set; }
        public string Cep { get; private set; }
        
        public Guid MotoristaId { get; private set; }
        public Motorista Motorista { get; private set; }

        public void Incluir(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }

        public void Alterar(
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string municipio,
            string uf,
            string cep,
            DateTime? dataAlteracao
            )
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Municipio = municipio;
            Uf = uf;
            Cep = cep;
            DataAlteracao = dataAlteracao;
        }
    }
}
