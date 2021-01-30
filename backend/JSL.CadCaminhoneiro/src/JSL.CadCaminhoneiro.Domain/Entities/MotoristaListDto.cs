using System;

namespace JSL.CadCaminhoneiro.Domain.Entities
{
    public class MotoristaListDto
    {
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

        public Endereco Endereco { get; set; }
        public Habilitacao Habilitacao { get; set; }
        public Caminhao Caminhao { get; set; }
    }
}
