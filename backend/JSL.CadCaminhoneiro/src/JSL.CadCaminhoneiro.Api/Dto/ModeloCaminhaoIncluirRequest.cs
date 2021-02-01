using System;
using FluentValidation;

namespace JSL.CadCaminhoneiro.Api.Dto
{
    public class ModeloCaminhaoIncluirRequest
    {
        public string Descricao { get; set; }
        public string Ano { get; set; }
        public Guid MarcaCaminhaoId { get; set; }
    }

    public class ModeloCaminhaoIncluirRequestValidator : AbstractValidator<ModeloCaminhaoIncluirRequest>
    {
        public ModeloCaminhaoIncluirRequestValidator()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty()
                .WithMessage("O campo Descrição é obrigatório").Length(3, 50);
            
            RuleFor(p => p.Ano)
                .NotEmpty()
                .WithMessage("O campo Ano é obrigatório").Length(4);

            RuleFor(p => p.MarcaCaminhaoId)
                .NotEqual(Guid.Empty)
                .WithMessage("O campo Id da marca do caminhão é obrigatório");
        }
    }
}
