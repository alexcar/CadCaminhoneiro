using System;
using FluentValidation;

namespace JSL.CadCaminhoneiro.Api.Dto
{
    public class MotoristaAlterarRequestValidator : AbstractValidator<MotoristaAlterarRequest>
    {
        public MotoristaAlterarRequestValidator()
        {
            // motorista
            RuleFor(p => p.Id).NotEqual(Guid.Empty)
                .WithMessage("O Id do motorista é obrigatório");

            RuleFor(p => p.Nome).NotEmpty()
                .WithMessage("O nome do motorista é obrigatório");

            RuleFor(p => p.Cpf).NotEmpty()
                .WithMessage("O número do Cpf é obrigatório");

            RuleFor(p => p.DataNascimento).NotEmpty()
                .WithMessage("A data de nascimento é obrigatório");

            RuleFor(p => p.NomePai).NotEmpty()
                .WithMessage("O nome do pai é obrigatório");

            RuleFor(p => p.NomeMae).NotEmpty()
                .WithMessage("O nome da mãe é obrigatório");

            RuleFor(p => p.Naturalidade).NotEmpty()
                .WithMessage("A naturalidade é obrigatório");

            RuleFor(p => p.NumeroRegistroGeral).NotEmpty()
                .WithMessage("O número do RG é obrigatório");

            RuleFor(p => p.OrgaoExpedicaoRegistroGeral).NotEmpty()
                .WithMessage("O nome do orgão de expedição do RG é obrigatório");

            RuleFor(p => p.DataExpedicaoRegistroGeral).NotEmpty()
                .WithMessage("A data de expedição do RG é obrigatório");


            // endereço
            RuleFor(p => p.EnderecoId).NotEqual(Guid.Empty)
                .WithMessage("O Id do endereço é obrigatório");

            RuleFor(p => p.Logradouro).NotEmpty()
                .WithMessage("O logradouro é obrigatório");

            RuleFor(p => p.Numero).NotEmpty()
                .WithMessage("O número do endereço é obrigatório");

            RuleFor(p => p.Bairro).NotEmpty()
                .WithMessage("O bairro é obrigatório");

            RuleFor(p => p.Municipio).NotEmpty()
                .WithMessage("O município é obrigatório");

            RuleFor(p => p.Uf).NotEmpty()
                .WithMessage("O estado do endereço é obrigatório");

            RuleFor(p => p.Cep).NotEmpty()
                .WithMessage("O cep do endereço é obrigatório");

            // habilitaçao
            RuleFor(p => p.HabilitacaoId).NotEqual(Guid.Empty)
                .WithMessage("O Id da habilitação é obrigatório");

            RuleFor(p => p.NumeroRegistroHabilitacao).NotEmpty()
                .WithMessage("O número do registro da habilitação é obrigatório");

            RuleFor(p => p.CategoriaHabilitacao).NotEmpty()
                .WithMessage("A categoria da habilitação é obrigatório");

            RuleFor(p => p.DataPrimeiraHabilitacao).NotEmpty()
                .WithMessage("A data da primeira habilitação é obrigatório");

            RuleFor(p => p.DataValidadeHabilitacao).NotEmpty()
                .WithMessage("A data de validade da habilitação é obrigatório");

            RuleFor(p => p.DataEmissaoHabilitacao).NotEmpty()
                .WithMessage("A data de emissão da habilitação é obrigatório");

            RuleFor(p => p.Placa).NotEmpty()
                .WithMessage("A placa do caminhão é obrigatório");

            // caminhão
            RuleFor(p => p.CaminhaoId).NotEqual(Guid.Empty)
                .WithMessage("O Id do caminhão é obrigatório");

            RuleFor(p => p.Eixo).NotEmpty()
                .WithMessage("A quantidade de eixo do caminhão é obrigatório");

            RuleFor(p => p.MarcaCaminhaoId).NotEqual(Guid.Empty)
                .WithMessage("A marca do caminhão é obrigatório");

            RuleFor(p => p.ModeloCaminhaoId).NotEqual(Guid.Empty)
                .WithMessage("O modelo do caminhão é obrigatório");

        }
    }
}
