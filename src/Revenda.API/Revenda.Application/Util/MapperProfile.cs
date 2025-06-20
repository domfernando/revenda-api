using AutoMapper;
using Revenda.Domain.Entities;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;

namespace Revenda.Application.Util
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cliente, CreateClienteRequest>();

            CreateMap<Cliente, UpdateClienteRequest>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.TipoPessoa, opt => opt.MapFrom(src => src.TipoPessoa))
              .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
              .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.NomeCompleto))
              .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
              .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<EnderecoCliente, EnderecoClienteRequest>();
            CreateMap<ContatoCliente, ContatoClienteRequest>();

            CreateMap<ClienteResponse, Cliente>();

            CreateMap<Revenda.Domain.Entities.Revenda, CreateRevendaRequest>();

            CreateMap<Revenda.Domain.Entities.Revenda, UpdateRevendaRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.RazaoSocial))
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.NomeFantasia))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CNPJ))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<EnderecoRevenda, RevendaEnderecoRequest>();
            CreateMap<ContatoRevenda, RevendaContatoRequest>();

            CreateMap<RevendaResponse, Revenda.Domain.Entities.Revenda>();

            CreateMap<Pedido, CreatePedidoRequest>();
            CreateMap<Pedido, UpdatePedidoRequest>();
            CreateMap<PedidoResponse, Pedido>();
        }
    }
}
