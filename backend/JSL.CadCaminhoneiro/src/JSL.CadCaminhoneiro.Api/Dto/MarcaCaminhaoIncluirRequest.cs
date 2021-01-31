using FluentValidation;

namespace JSL.CadCaminhoneiro.Api.Dto
{
    public class MarcaCaminhaoIncluirRequest
    {
        public string Descricao { get; set; }
    }

    public class MarcaCaminhaoIncluirRequestValidator: AbstractValidator<MarcaCaminhaoIncluirRequest>
    {
        public MarcaCaminhaoIncluirRequestValidator()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty()
                .Length(3, 50)
                .WithMessage("O campo Descrição é obrigatório");
        }
    }
}
