using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;
using Revenda.Domain.Entities;
using Revenda.Infra.Persistence.Context;

namespace Revenda.Infra.Persistence.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;

        public PedidoRepository(DatabaseContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PedidoResponse> AddAsync(CreatePedidoRequest request)
        {
            var novo = _mapper.Map<Domain.Entities.Pedido>(request);
            novo.Criado = DateTime.Now;
            var retorno = await _db.Pedido.AddAsync(novo);
            return _mapper.Map<PedidoResponse>(retorno.Entity);
        }

        public async Task<List<PedidoResponse>> GetAll(GetPedidoRequest condition)
        {
            var dados = await _db.Pedido
              .Where(x => (condition.Id == null || x.ID == condition.Id))
              .ToListAsync();
            return dados == null ? null : _mapper.Map<List<PedidoResponse>>(dados);
        }

        public async Task<PedidoResponse> GetById(int id)
        {
            var obj = await _db.Pedido.Where(x => x.ID == id).FirstOrDefaultAsync();
            return obj == null ? null : _mapper.Map<PedidoResponse>(obj);
        }

        public async Task<List<AbastecimentoResponse>> GetPedidosAbastecimento(ProcessaAbastecimentosRequest request)
        {
            var dados = await _db.Pedido
                .Include(p => p.Itens)
                .Where(p => p.TipoPedido == 2)
                .OrderByDescending(p => p.Data)
                .Take(request.Quantidade)
                .Select(p => new AbastecimentoResponse
                {
                    Id = p.ID,
                    Data = p.Data,
                    ValorTotal = p.Itens.Sum(i => i.ValorTotal),
                    Itens = _mapper.Map<List<PedidoItemRequest>>(p.Itens)
                }).ToListAsync();

            return dados;
        }

        public async Task<AbastecimentoRequest> GetToAbastecimento(int pedidoId)
        {
            var dados = await _db.Pedido
                        .Include(p => p.Itens)
                        .Where(p => p.ID == pedidoId)
                        .Select(p => new AbastecimentoRequest
                        {
                            PedidoId = p.ID,
                            RevendaId = (int)p.RevendaID,
                            Data = p.Data,
                            ValorTotal = p.Itens.Sum(i => i.ValorTotal),
                            Itens = _mapper.Map<List<PedidoItemRequest>>(p.Itens)
                        }).FirstOrDefaultAsync();
            return dados;
        }

        public async Task<PedidoResponse> UpdateAsync(UpdatePedidoRequest request)
        {
            var original = await _db.Pedido.FindAsync(request.Id);
            if (original == null)
                throw new KeyNotFoundException($"Pedido com ID {request.Id} não encontrado.");

            _mapper.Map(request, original);
            original.Alterado = DateTime.Now;
            _db.Pedido.Update(original);
            return _mapper.Map<PedidoResponse>(original);
        }

        public Task<Pedido> UpdateToAbastecimento(Pedido rquest)
        {
            throw new NotImplementedException();
        }
    }
}
