using FluentValidation;
using Revenda.Application.DTOs.Request;

namespace Revenda.Application.Validators
{
    public class CreateRevendaRequestValidator : AbstractValidator<CreateRevendaRequest>
    {
        public CreateRevendaRequestValidator()
        {
            RuleFor(x => x.CNPJ)
                .Must(cnpj => Application.Util.Tools.CNPJIsValid(cnpj)).WithMessage("CNPJ inválido");

            RuleFor(x => x.NomeFantasia)
             .NotEmpty().WithMessage("Nome fantasia é obrigatório")
             .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres")
             .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome deve conter apenas letras");

            RuleFor(x => x.RazaoSocial)
                .NotEmpty().WithMessage("Razão Social é obrigatório")
                .Length(2, 100).WithMessage("Razão Social deve ter entre 2 e 100 caracteres")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome deve conter apenas letras");
        }
    }

    public class UpdateRevendaRequestValidator : AbstractValidator<UpdateRevendaRequest>
    {
        public UpdateRevendaRequestValidator()
        {
            RuleFor(x => x.CNPJ)
               .Must(cnpj => Application.Util.Tools.CNPJIsValid(cnpj)).WithMessage("CNPJ inválido");

            RuleFor(x => x.NomeFantasia)
             .NotEmpty().WithMessage("Nome fantasia é obrigatório")
             .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres")
             .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome deve conter apenas letras");

            RuleFor(x => x.RazaoSocial)
                .NotEmpty().WithMessage("Razão Social é obrigatório")
                .Length(2, 100).WithMessage("Razão Social deve ter entre 2 e 100 caracteres")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome deve conter apenas letras");
        }
    }
}
