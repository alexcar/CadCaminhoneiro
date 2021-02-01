using System;

namespace JSL.CadCaminhoneiro.Api.Dto
{
    public class MotoristaAlterarRequest
    {
        // TODO: Verificar o que vai acontecer quando passar o ID das entidades 
        
        // motorista
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string Naturalidade { get; set; }
        public string NumeroRegistroGeral { get; set; }
        public string OrgaoExpedicaoRegistroGeral { get; set; }
        public DateTime DataExpedicaoRegistroGeral { get; set; }

        // endereco
        public Guid EnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }

        // habilitacao
        public Guid HabilitacaoId { get; set; }
        public string NumeroRegistroHabilitacao { get; set; }
        public string CategoriaHabilitacao { get; set; }
        public DateTime DataPrimeiraHabilitacao { get; set; }
        public DateTime DataValidadeHabilitacao { get; set; }
        public DateTime DataEmissaoHabilitacao { get; set; }
        public string ObservacaoHabilitacao { get; set; }

        // caminhao
        public Guid CaminhaoId { get; set; }
        public string Placa { get; set; }
        public byte Eixo { get; set; }
        public string CaminhaoObservacao { get; set; }
        public Guid MarcaCaminhaoId { get; set; }
        public Guid ModeloCaminhaoId { get; set; }
    }
}
