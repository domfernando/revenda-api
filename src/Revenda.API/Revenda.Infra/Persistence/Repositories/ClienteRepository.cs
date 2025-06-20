using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;
using Revenda.Infra.Persistence.Context;

namespace Revenda.Infra.Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;

        public ClienteRepository(DatabaseContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ClienteResponse> AddAsync(CreateClienteRequest request)
        {
            var novo = _mapper.Map<Domain.Entities.Cliente>(request);
            novo.Criado = DateTime.Now;
            var retorno = await _db.Cliente.AddAsync(novo);
            return _mapper.Map<ClienteResponse>(retorno.Entity);
        }

        public async Task<List<ClienteResponse>?> GetAll(GetClienteRequest condition)
        {
            var dados = await _db.Cliente
                .Where(x => (condition.Id == null || x.ID == condition.Id) &&
                            (string.IsNullOrEmpty(condition.Nome) || x.Nome.Contains(condition.Nome)))
                .ToListAsync();
            return dados == null ? null : _mapper.Map<List<ClienteResponse>>(dados);
        }

        public async Task<ClienteResponse> GetByID(int id)
        {
            var obj = await _db.Cliente.Where(x => x.ID == id).FirstOrDefaultAsync();
            return obj == null ? null : _mapper.Map<ClienteResponse>(obj);
        }

        public async Task<ClienteResponse> UpdateAsync(UpdateClienteRequest request)
        {
            var original = await _db.Cliente.FindAsync(request.Id);
            if (original == null)
                throw new KeyNotFoundException($"Cliente com ID {request.Id} não encontrado.");

            _mapper.Map(request, original);
            original.Alterado = DateTime.Now;
            _db.Cliente.Update(original);
            return _mapper.Map<ClienteResponse>(original);
        }
    }
}
