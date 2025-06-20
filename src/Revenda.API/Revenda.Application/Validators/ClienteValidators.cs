using FluentValidation;
using Revenda.Application.DTOs.Request;

namespace Revenda.Application.Validators
{
    public class CreateClienteRequestValidator : AbstractValidator<CreateClienteRequest>
    {
        public CreateClienteRequestValidator()
        {
            RuleFor(x => x.TipoPessoa)
                .NotEmpty().WithMessage("Tipo de pessoa é obrigatório")
                .Must(tipo => tipo == 1 || tipo == 2)
                .WithMessage("Tipo de pessoa deve ser 1 (Pessoa Jurídica) ou 2 (Pessoa Física)");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("Documento é obrigatório")
                .Must(doc => Application.Util.Tools.CNPJIsValid(doc) || Application.Util.Tools.CPFIsValid(doc)).WithMessage("Documento inválido");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(tipo => tipo.TipoPessoa == 1 ? "Razão Social é obrigatório" : "Nome é obrigatório")
                .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome completo deve conter apenas letras");

            RuleFor(x => x.NomeCompleto)
                 .NotEmpty().WithMessage(tipo => tipo.TipoPessoa == 1 ? "Nome fantasia é obrigatório" : "Nome completo é obrigatório")
                 .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres")
                 .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome completo deve conter apenas letras");
        }
    }

    public class UpdateClienteRequestValidator : AbstractValidator<UpdateClienteRequest>
    {
        public UpdateClienteRequestValidator()
        {
            RuleFor(x => x.TipoPessoa)
              .IsInEnum()
              .WithMessage("Tipo de pessoa deve ser 1 (Pessoa Jurídica) ou 2 (Pessoa Física)");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("Documento é obrigatório")
                .Must(doc => Application.Util.Tools.CNPJIsValid(doc) || Application.Util.Tools.CPFIsValid(doc)).WithMessage("Documento inválido");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(tipo => tipo.TipoPessoa == 1 ? "Razão Social é obrigatório" : "Nome é obrigatório")
                .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome completo deve conter apenas letras");

            RuleFor(x => x.NomeCompleto)
                 .NotEmpty().WithMessage(tipo => tipo.TipoPessoa == 1 ? "Nome fantasia é obrigatório" : "Nome completo é obrigatório")
                 .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres")
                 .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome completo deve conter apenas letras");

            RuleFor(x => x.Enderecos)
                .NotNull().WithMessage("Lista de endereços não pode ser nula")
                .NotEmpty().WithMessage("Lista de endereços não pode estar vazia")
                .Must(enderecos => enderecos.Count > 1).WithMessage("Pelo menos um endereço deve ser informado");

            RuleFor(x => x.Contatos)
              .NotNull().WithMessage("Lista de contatos não pode ser nula")
              .NotEmpty().WithMessage("Lista de contatos não pode estar vazia")
              .Must(contatos => contatos.Count > 1).WithMessage("Pelo menos um contato deve ser informado");
        }
    }
}
