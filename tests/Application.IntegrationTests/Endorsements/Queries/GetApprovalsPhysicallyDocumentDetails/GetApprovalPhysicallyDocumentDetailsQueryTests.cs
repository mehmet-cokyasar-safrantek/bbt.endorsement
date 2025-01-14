﻿using System.Threading.Tasks;
using Application.Endorsements.Queries.GetApprovalsPhysicallyDocumentDetails;
using NUnit.Framework;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Endorsements.Queries.GetApprovalsPhysicallyDocumentDetails
{
    public class GetApprovalPhysicallyDocumentDetailsQueryTests:TestBase
    {
        [Test]
        [TestCase("1")]
        public async Task GetApprovalPhysicallyDocumentDetailsQueryTestAsync(int approvalId)
        {
            var response = await SendAsync(new GetApprovalPhysicallyDocumentDetailsQuery { ApprovalId = approvalId });
            Assert.IsNotNull(approvalId);
        }
    }
}
