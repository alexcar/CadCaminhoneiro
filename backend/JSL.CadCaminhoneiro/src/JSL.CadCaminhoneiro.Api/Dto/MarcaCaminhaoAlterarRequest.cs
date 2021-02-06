using System;
using FluentValidation;

namespace JSL.CadCaminhoneiro.Api.Dto
{
    public class MarcaCaminhaoAlterarRequest
    {

        public Guid Id { get; set; }
        public string Descricao { get; set; }
    }

    public class MarcaCaminhaoAlterarRequestValidator : AbstractValidator<MarcaCaminhaoAlterarRequest>
    {
        public MarcaCaminhaoAlterarRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O campo Id é obrigatório");

            RuleFor(p => p.Descricao)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("O campo Descrição é obrigatório");
        }
    }
}
