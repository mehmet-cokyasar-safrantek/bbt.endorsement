using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.OrderForms.Queries.GetForms
{
    public class GetFormQuery : IRequest<Response<List<GetFormDto>>>
    {
       

    }
    /// <summary>
    /// Form Listesi
    /// </summary>
    public class GetFormQueryHandler : IRequestHandler<GetFormQuery, Response<List<GetFormDto>>>
    {
        private IApplicationDbContext _context;

        public GetFormQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<GetFormDto>>> Handle(GetFormQuery request, CancellationToken cancellationToken)
        {
            var list = _context.Forms.Select(x => new GetFormDto { FormId = x.FormId, FormName = x.Title }).OrderBy(x => x.FormName).ToList();
            return Response<List<GetFormDto>>.Success(list, 200);
        }
    }
}
