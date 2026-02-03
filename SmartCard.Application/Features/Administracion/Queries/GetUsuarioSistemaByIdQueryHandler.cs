using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCard.Application.Common.Interfaces;
using SmartCard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCard.Application.Features.Administracion.Queries
{    public record GetUsuarioSistemaByIdQuery(int Id) : IRequest<UsuarioSistemaDto?>;
    public class GetUsuarioSistemaByIdQueryHandler : IRequestHandler<GetUsuarioSistemaByIdQuery, UsuarioSistemaDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUsuarioSistemaByIdQueryHandler(IApplicationDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<UsuarioSistemaDto?> Handle(GetUsuarioSistemaByIdQuery request, CancellationToken cancellationToken)
            => await _context.UsuarioSistema.AsNoTracking().Where(e => e.IdUsuarioSistema == request.Id).ProjectTo<UsuarioSistemaDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
    }
}
