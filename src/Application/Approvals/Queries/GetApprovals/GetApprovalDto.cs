﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Approvals.Queries.GetApprovals
{
    public class GetApprovalDto
    {
        public int ApprovalId { get; set; }
        public string ApprovalName { get; set; }
        public bool IsDocument { get; set; }
    }
}
