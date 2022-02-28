﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Events.Approvals;
using MediatR;

namespace Application.Approvals.Commands.CreateApprovalCommands;

public class CreateApprovalCommand : IRequest<Response<List<CreateApprovalCommandDto>>>
{
    /// <summary>
    /// InstanceId
    /// </summary>
    public string InstanceId { get; set; }
    /// <summary>
    ///Baslik
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// İşlem
    /// </summary>
    public string Process { get; set; }
    /// <summary>
    /// Aşama
    /// </summary>
    public string Stage { get; set; }
    /// <summary>
    /// İşlem No
    /// </summary>
    public string TransactionNumber { get; set; }
    /// <summary>
    /// Geçerlilik
    /// </summary>
    public string TimeoutMinutes { get; set; }
    /// <summary>
    /// Hatırlatma Frekansı
    /// </summary>
    public string RetryFrequence { get; set; }
    /// <summary>
    /// Hatırlatma Sayısı
    /// </summary>
    public int MaxRetryCount { get; set; }
}

public class CreateApprovalCommandHandler : IRequestHandler<CreateApprovalCommand, Response<List<CreateApprovalCommandDto>>>
{
    private readonly IApplicationDbContext _context;

    public CreateApprovalCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Yeni Onaylama Emri Ekleme
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Response<List<CreateApprovalCommandDto>>> Handle(CreateApprovalCommand request, CancellationToken cancellationToken)
    {
        var list = new List<CreateApprovalCommandDto>();
        return Response<List<CreateApprovalCommandDto>>.Success(list, 200);
    }
    //public async Task<Response<bool>> Handle(CreateApprovalCommand request, CancellationToken cancellationToken)
    //{
    //    var entity = new Domain.Entities.Approval
    //    {
    //        ApprovalId = Guid.NewGuid().ToString(),
    //        InstanceId = request.InstanceId,
    //        ApprovalTitle = request.Title
    //    };

    //    entity.DomainEvents.Add(new ApprovalCreateEvent(entity));
    //    _context.Approvals.Add(entity);
    //    var i = _context.SaveChanges();

    //    return Response<bool>.Success(i > 0, 200);
    //}
}

