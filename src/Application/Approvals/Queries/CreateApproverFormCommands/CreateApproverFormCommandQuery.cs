﻿using Application.Common.Models;
using MediatR;

namespace Application.Approvals.Queries.CreateApproverFormCommands
{
    public class CreateApproverFormCommandQuery : IRequest<Response<List<CreateApproverFormCommandDto>>>
    {
        public int ApprovalId { get; set; }
        public string TC { get; set; }
        public string CustomerNumber { get; set; }
    }

    public class CreateApproverFormCommandQueryHandler : IRequestHandler<CreateApproverFormCommandQuery, Response<List<CreateApproverFormCommandDto>>>
    {
        public async Task<Response<List<CreateApproverFormCommandDto>>> Handle(CreateApproverFormCommandQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CreateApproverFormCommandDto>();
            return Response<List<CreateApproverFormCommandDto>>.Success(list, 200);
        }
    }
}
