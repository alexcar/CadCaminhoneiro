using System;
using FluentValidation;

namespace JSL.CadCaminhoneiro.Api.Dto
{
    public class MotoristaIncluirRequestValidator : AbstractValidator<MotoristaIncluirRequest>
    {
        public MotoristaIncluirRequestValidator()
        {
            RuleFor(p => p.Nome).NotEmpty()
                .MaximumLength(100)
                .WithMessage("O nome do motorista é obrigatório");

            RuleFor(p => p.Cpf).NotEmpty()
                .MaximumLength(11)
                .WithMessage("O número do Cpf é obrigatório");

            RuleFor(p => p.DataNascimento).NotEmpty()
                .WithMessage("A data de nascimento é obrigatório");

            RuleFor(p => p.NomePai).NotEmpty()
                .MaximumLength(100)
                .WithMessage("O nome do pai é obrigatório");

            RuleFor(p => p.NomeMae).NotEmpty()
                .MaximumLength(100)
                .WithMessage("O nome da mãe é obrigatório");

            RuleFor(p => p.Naturalidade).NotEmpty()
                .MaximumLength(19)
                .WithMessage("A naturalidade é obrigatório");

            RuleFor(p => p.NumeroRegistroGeral).NotEmpty()
                .MaximumLength(20)
                .WithMessage("O número do RG é obrigatório");

            RuleFor(p => p.OrgaoExpedicaoRegistroGeral).NotEmpty()
                .MaximumLength(20)
                .WithMessage("O nome do orgão de expedição do RG é obrigatório");

            RuleFor(p => p.DataExpedicaoRegistroGeral).NotEmpty()
                .WithMessage("A data de expedição do RG é obrigatório");

            RuleFor(p => p.Logradouro).NotEmpty()
                .MaximumLength(100)
                .WithMessage("O logradouro é obrigatório");

            RuleFor(p => p.Numero).NotEmpty()
                .MaximumLength(10)
                .WithMessage("O número do endereço é obrigatório");

            RuleFor(p => p.Complemento)
                .MaximumLength(30);

            RuleFor(p => p.Bairro).NotEmpty()
                .MaximumLength(50)
                .WithMessage("O bairro é obrigatório");

            RuleFor(p => p.Municipio).NotEmpty()
                .MaximumLength(50)
                .WithMessage("O município é obrigatório");

            RuleFor(p => p.Uf).NotEmpty()
                .MaximumLength(2)
                .WithMessage("O estado do endereço é obrigatório");

            RuleFor(p => p.Cep).NotEmpty()
                .MaximumLength(8)
                .WithMessage("O cep do endereço é obrigatório");

            RuleFor(p => p.NumeroRegistroHabilitacao).NotEmpty()
                .MaximumLength(20)
                .WithMessage("O número do registro da habilitação é obrigatório");

            RuleFor(p => p.CategoriaHabilitacao).NotEmpty()
                .MaximumLength(7)
                .WithMessage("A categoria da habilitação é obrigatório");

            RuleFor(p => p.DataPrimeiraHabilitacao).NotEmpty()
                .WithMessage("A data da primeira habilitação é obrigatório");

            RuleFor(p => p.DataValidadeHabilitacao).NotEmpty()
                .WithMessage("A data de validade da habilitação é obrigatório");

            RuleFor(p => p.DataEmissaoHabilitacao).NotEmpty()
                .WithMessage("A data de emissão da habilitação é obrigatório");

            RuleFor(p => p.ObservacaoHabilitacao).MaximumLength(50);

            RuleFor(p => p.Placa).NotEmpty()
                .MaximumLength(8)
                .WithMessage("A placa do caminhão é obrigatório");

            RuleFor(p => p.CaminhaoObservacao).MaximumLength(30);

            RuleFor(p => p.Eixo).NotEmpty()
                .WithMessage("A quantidade de eixo do caminhão é obrigatório");

            RuleFor(p => p.MarcaCaminhaoId).NotEqual(Guid.Empty)
                .WithMessage("A marca do caminhão é obrigatório");

            RuleFor(p => p.ModeloCaminhaoId).NotEqual(Guid.Empty)
                .WithMessage("O modelo do caminhão é obrigatório");

        }
    }
}
