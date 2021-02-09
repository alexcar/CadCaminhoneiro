using System.Linq;
using JSL.CadCaminhoneiro.Domain.Entities;

namespace JSL.CadCaminhoneiro.Data.Repository.QueryObjects
{
    public static class MotoristaListDtoSelect
    {
        public static IQueryable<MotoristaListDto> MapMotoristaToDto(this IQueryable<Motorista> motoristas)
        {
            return motoristas.Select(p => new MotoristaListDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Cpf = p.Cpf,
                DataNascimento = p.DataNascimento,
                NomePai = p.NomePai,
                NomeMae = p.NomeMae,
                Naturalidade = p.Naturalidade,
                NumeroRegistroGeral = p.NumeroRegistroGeral,
                OrgaoExpedicaoRegistroGeral = p.OrgaoExpedicaoRegistroGeral,
                DataExpedicaoRegistroGeral = p.DataExpedicaoRegistroGeral,
                EnderecoDto = new EnderecoDto
                {
                    Id = p.Endereco.Id,
                    MotoristaId = p.Id,
                    Logradouro = p.Endereco.Logradouro,
                    Numero = p.Endereco.Numero,
                    Complemento = p.Endereco.Complemento,
                    Bairro = p.Endereco.Bairro,
                    Municipio = p.Endereco.Municipio,
                    Uf = p.Endereco.Uf,
                    Cep = p.Endereco.Cep
                },
                HabilitacaoDto = new HabilitacaoDto
                {
                    Id = p.Habilitacao.Id,
                    MotoristaId = p.Id,
                    NumeroRegistro = p.Habilitacao.NumeroRegistro,
                    Categoria = p.Habilitacao.Categoria,
                    DataPrimeiraHabilitacao = p.Habilitacao.DataPrimeiraHabilitacao,
                    DataValidade = p.Habilitacao.DataValidade,
                    DataEmissao = p.Habilitacao.DataEmissao,
                    Observacao = p.Habilitacao.Observacao
                },
                CaminhaoDto = new CaminhaoDto
                {
                    Id = p.Caminhao.Id,
                    MotoristaId = p.Id,
                    Placa = p.Caminhao.Placa,
                    Eixo = p.Caminhao.Eixo,
                    Observacao = p.Caminhao.Observacao,
                    MarcaCaminhaoListDto = new MarcaCaminhaoListDto
                    {
                        Id = p.Caminhao.MarcaCaminhao.Id,
                        Descricao = p.Caminhao.MarcaCaminhao.Descricao
                    },
                    ModeloCaminhaoListDto = new ModeloCaminhaoListDto
                    {
                        Id = p.Caminhao.ModeloCaminhao.Id,
                        Descricao = p.Caminhao.ModeloCaminhao.Descricao,
                        Ano = p.Caminhao.ModeloCaminhao.Ano
                    }
                }
            });
        }
    }
}
