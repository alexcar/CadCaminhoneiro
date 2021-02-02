using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class MotoristaListDto
    {
        public MotoristaListDto()
        {
            EnderecoDto = new EnderecoDto();
            HabilitacaoDto = new HabilitacaoDto();
            CaminhaoDto = new CaminhaoDto();
        }
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

        public EnderecoDto EnderecoDto { get; set; }
        public HabilitacaoDto HabilitacaoDto { get; set; }
        public CaminhaoDto CaminhaoDto { get; set; }
    }

    public class EnderecoDto
    {
        public Guid Id { get; set; }
        public Guid MotoristaId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public MotoristaListDto MotoristaListDto { get; set; }
    }

    public class HabilitacaoDto
    {
        public Guid Id { get; set; }
        public Guid MotoristaId { get; set; }
        public string NumeroRegistro { get; set; }
        public string Categoria { get; set; }
        public DateTime DataPrimeiraHabilitacao { get; set; }
        public DateTime DataValidade { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Observacao { get; set; }
        public MotoristaListDto MotoristaListDto { get; set; }
    }

    public class CaminhaoDto
    {
        public Guid Id { get; set; }
        public Guid MotoristaId { get; set; }
        public string Placa { get; set; }
        public byte Eixo { get; set; }
        public string Observacao { get; set; }
        public MotoristaListDto MotoristaListDto { get; set; }
    }
}
