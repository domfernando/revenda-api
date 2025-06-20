using FluentValidation;
using Revenda.Application.DTOs.Request;

namespace Revenda.Application.Validators
{
    public class CreatePedidoRequestValidator : AbstractValidator<CreatePedidoRequest>
    {
        public CreatePedidoRequestValidator()
        {
            RuleFor(x => x.TipoPedido)
                .IsInEnum()
                .WithMessage("Tipo de pessoa deve ser 1 (Fornecimento) ou 2 (Abastecimento)");

            When((x => x.TipoPedido == 1), () =>
            {
                RuleFor(x => x.ClienteId)
                    .NotNull().WithMessage("ClienteId é obrigatório para pedidos de fornecimento")
                    .GreaterThan(0).WithMessage("ClienteId deve ser maior que zero");
            });
            When((x => x.TipoPedido == 2), () =>
            {
                RuleFor(x => x.RevendaId)
                    .NotNull().WithMessage("RevendaId é obrigatório para pedidos de abastecimento")
                    .GreaterThan(0).WithMessage("RevendaId deve ser maior que zero");
            });
        }
    }

    public class UpdatePedidoRequestValidator : AbstractValidator<UpdatePedidoRequest>
    {
        public UpdatePedidoRequestValidator()
        {
            RuleFor(x => x.TipoPedido)
              .IsInEnum()
              .WithMessage("Tipo de pessoa deve ser 1 (Fornecimento) ou 2 (Abastecimento)");

            When((x => x.TipoPedido == 1), () =>
            {
                RuleFor(x => x.ClienteId)
                    .NotNull().WithMessage("ClienteId é obrigatório para pedidos de fornecimento")
                    .GreaterThan(0).WithMessage("ClienteId deve ser maior que zero");

                RuleFor(x => x.Itens)
                    .NotEmpty().WithMessage("A lista de itens não pode estar vazia")
                    .Must(itens => itens.Count > 0)
                    .WithMessage("È necessário pelo menos 1 item para esse pedido");
            });
            When((x => x.TipoPedido == 2), () =>
            {
                RuleFor(x => x.RevendaId)
                    .NotNull().WithMessage("RevendaId é obrigatório para pedidos de abastecimento")
                    .GreaterThan(0).WithMessage("RevendaId deve ser maior que zero");

                RuleFor(x => x.Itens)
                 .NotEmpty().WithMessage("A lista de itens não pode estar vazia")
                 .Must(itens => itens.Count > 0)
                 .WithMessage("È necessário pelo menos 1 item para esse pedido")
                 .Must(itens => itens.Sum(item => item.Quantidade) > 100)
                 .WithMessage("Pedido mínimo: 100 unidades");
            });
        }
    }
}
