using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;
using Revenda.Infra.Persistence.Context;

namespace Revenda.Infra.Persistence.Repositories
{
    public class RevendaRepository : IRevendaRepository
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;

        public RevendaRepository(DatabaseContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<RevendaResponse> AddAsync(CreateRevendaRequest request)
        {
            var novo = _mapper.Map<Domain.Entities.Revenda>(request);
            novo.Criado = DateTime.Now;
            var retorno = await _db.Revenda.AddAsync(novo);
            return _mapper.Map<RevendaResponse>(retorno.Entity);
        }

        public async Task<List<RevendaResponse>> GetAllAsync(GetRevendaRequest condition)
        {
            var dados = await _db.Revenda
               .Where(x => (condition.Id == null || x.ID == condition.Id) &&
                           (string.IsNullOrEmpty(condition.Nome) || x.NomeFantasia.Contains(condition.Nome)))
               .ToListAsync();
            return dados == null ? null : _mapper.Map<List<RevendaResponse>>(dados);
        }

        public async Task<RevendaResponse> GetByIdAsync(int id)
        {
            var obj = await _db.Revenda.Where(x => x.ID == id).FirstOrDefaultAsync();
            return obj == null ? null : _mapper.Map<RevendaResponse>(obj);
        }

        public async Task<RevendaResponse> UpdateAsync(UpdateRevendaRequest request)
        {
            var original = await _db.Revenda.FindAsync(request.Id);
            if (original == null)
                throw new KeyNotFoundException($"Revenda com ID {request.Id} não encontrado.");

            _mapper.Map(request, original);
            original.Alterado = DateTime.Now;
            _db.Revenda.Update(original);
            return _mapper.Map<RevendaResponse>(original);
        }
    }
}
