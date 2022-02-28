﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using MediatR;

namespace Application.Approvals.Queries.GetMyApprovals
{
    public class GetMyApprovalQuery : IRequest<Response<List<GetMyApprovalDto>>>
    {
        /// <summary>
        /// InstanceId
        /// </summary>
        public string InstanceId { get; set; }
    }
    /// <summary>
    /// Onayladıklarım Listesi
    /// </summary>
    public class GetMyApprovalQueryHandler : IRequestHandler<GetMyApprovalQuery, Response<List<GetMyApprovalDto>>>
    {
        public async Task<Response<List<GetMyApprovalDto>>> Handle(GetMyApprovalQuery request, CancellationToken cancellationToken)
        {
            var list = new List<GetMyApprovalDto>();
            return Response<List<GetMyApprovalDto>>.Success(list, 200);
        }
    }


}
