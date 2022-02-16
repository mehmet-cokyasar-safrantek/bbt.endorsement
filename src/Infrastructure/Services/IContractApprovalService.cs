﻿using Application.Approvals.Commands.CreateApprovalCommands;
using Infrastructure.Dtos;
using Infrastructure.ZeebeServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Zeebe.Client.Api.Worker;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Infrastructure.Services;

public interface IContractApprovalService
{
    void StartWorkers();
}

public class ContractApprovalService : IContractApprovalService
{
    private ILogger _logger;
    private IZeebeService _zeebeService;
    private static readonly string WorkerName = Environment.MachineName;
    private IServiceProvider _provider = null!;
    private ISender _mediator = null!;

    public ContractApprovalService(ILogger<ContractApprovalService> logger, IZeebeService zeebeService, IServiceProvider provider)
    {
        _logger = logger;
        _zeebeService = zeebeService;
        _provider = provider;
        _mediator = _provider.CreateScope().ServiceProvider.GetRequiredService<ISender>();
    }

    public void StartWorkers()
    {
        ApproveContract();
        ConsumeCallback();
        LoadContactInfo();
        SaveEntity();
        SendOtp();
        SendPush();
        UpdateEntity();
        DeleteEntity();
        ErrorHandler();
    }

    private void ErrorHandler()
    {
        _logger.LogInformation("ErrorHandler Worker registered ");

        CreateWorker("ErrorHandler", async (jobClient, job) =>
        {
            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);
            string data = JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
            _logger.LogInformation($"ErrorEntity data: '{variables.InstanceId}'");

            await jobClient.NewCompleteJobCommand(job.Key)
                .Variables("{\"Approve\":\"" + false + "\"}")
                .Send();
        });
    }

    private void SaveEntity()
    {
        _logger.LogInformation("SaveEntity Worker registered ");
        //var response = await _mediator.Send(new CreateApplyContractCommand
        //{
        //    InstanceId = "928a9fd5-1549-4342-8bc0-a154e8692928"
        //});

        CreateWorker("SaveEntity", async (jobClient, job) =>
        {
            Dictionary<string, object> customHeaders = JsonSerializer.Deserialize<Dictionary<string, object>>(job.CustomHeaders);
            Dictionary<string, object> _variables = JsonSerializer.Deserialize<Dictionary<string, object>>(job.Variables);

            // var state = customHeaders["State"].ToString();

            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);
            if (variables != null)
            {
                //var _mediator = _provider.CreateScope().ServiceProvider.GetRequiredService<ISender>();
                //var response = await _mediator.Send(new CreateApprovalCommand
                //{
                //    InstanceId = variables.InstanceId,
                //    Title = ""
                //});
                variables.IsProcess = true;
            }
            string data = JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });

            _logger.LogInformation($"SaveEntity data: '{variables.InstanceId}'");
            await jobClient.NewCompleteJobCommand(job.Key)
                .Variables(data)
                .Send();
        });
    }


    private void LoadContactInfo()
    {
        //_logger.LogInformation("LoadContactInfo Worker registered ");

        CreateWorker("LoadContactInfo", async (jobClient, job) =>
        {

            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);
            if (variables != null)
                variables.RetryEnd = true;
            string data = JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });

            _logger.LogInformation($"LoadContactInfo data: '{variables.InstanceId}'");
            await jobClient.NewCompleteJobCommand(job.Key)
                .Variables(data)
                .Send();
        });
    }
    private void SendOtp()
    {
        _logger.LogInformation("SendOtp Worker registered ");

        CreateWorker("SendOtp", async (jobClient, job) =>
        {
            try
            {
                var contractApprovalData = new ContractApprovalData();
                //var customer = contractApprovalData.Request.Customer;

                var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);

                if (variables != null)
                    variables.Limit += 1;
                variables.IsProcess = true;

                string data = System.Text.Json.JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });

                _logger.LogInformation($"SendOtp data: '{variables.InstanceId}'");

                await jobClient.NewCompleteJobCommand(job.Key)
                    .Variables(data)
                    .Send();
            }
            catch (Exception ex)
            {
                await jobClient.NewThrowErrorCommand(job.Key).ErrorCode("500").ErrorMessage(ex.Message).Send();
            }
        });
    }

    private void SendPush()
    {
        _logger.LogInformation("SendPush Worker registered ");

        CreateWorker("SendPush", async (jobClient, job) =>
        {
            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);

            try
            {
                throw new Exception("Hata");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }


            if (variables != null)
                variables.Limit += 1;
            variables.IsProcess = true;

            string data = System.Text.Json.JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
            _logger.LogInformation($"SendPush data: '{variables.InstanceId}'");

            await jobClient.NewCompleteJobCommand(job.Key)
                .Variables(data)
                .Send();
        });
    }

    private void UpdateEntity()
    {
        _logger.LogInformation("UpdateEntity Worker registered ");

        CreateWorker("UpdateEntity", async (jobClient, job) =>
        {
            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);
            if (variables != null)
            {
                variables.Completed = false;
                string data = System.Text.Json.JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
                _logger.LogInformation($"UpdateEntity data: '{variables.InstanceId}'");

                await jobClient.NewCompleteJobCommand(job.Key)
                    .Variables(data) // "{\"IsTimeOut\":\"" + true + "\"}"
                    .Send();
            }
        });
    }

    private void ApproveContract()
    {
        _logger.LogInformation("ApproveContract Worker registered ");

        CreateWorker("ApproveContract", async (jobClient, job) =>
        {
            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);
            if (variables != null)
            {
                variables.Approved = true;
                variables.Completed = true;
                variables.IsProcess = true;
            }
            string data = JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
            _logger.LogInformation($"ApproveContract data: '{variables.InstanceId}'");

            await jobClient.NewCompleteJobCommand(job.Key)
                .Variables(data)
                .Send();

        });
    }
    private void DeleteEntity()
    {
        _logger.LogInformation("DeleteEntity Worker registered ");

        CreateWorker("DeleteEntity", async (jobClient, job) =>
        {
            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);
            string data = JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
            _logger.LogInformation($"DeleteEntity data: '{variables.InstanceId}'");
            await jobClient.NewCompleteJobCommand(job.Key)
                      .Variables("{\"Approve\":\"" + true + "\"}")
                      .Send();

        });
    }

    private void ConsumeCallback()
    {
        _logger.LogInformation("ConsumeCallback Worker registered ");

        CreateWorker("ConsumeCallback", async (jobClient, job) =>
        {
            var variables = JsonConvert.DeserializeObject<ContractApprovalData>(job.Variables);
            string data = JsonSerializer.Serialize(variables, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
            _logger.LogInformation($"ConsumeCallback data: '{variables.InstanceId}'");
            await jobClient.NewCompleteJobCommand(job.Key)
                      .Variables("{\"Approve\":\"" + true + "\"}")
                      .Send();

        });
    }


    private void CreateWorker(String jobType, JobHandler handleJob)
    {
        _zeebeService.Client().NewWorker()
               .JobType(jobType)
               .Handler(handleJob)
               .MaxJobsActive(5)
               .Name(WorkerName)
               .AutoCompletion()
               .PollInterval(TimeSpan.FromSeconds(50))
               .PollingTimeout(TimeSpan.FromSeconds(50))
               .Timeout(TimeSpan.FromSeconds(10))
               .Open();
    }
}